using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Enum;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service.Contracts;
using Sicotyc.ActionFilters;
using Sicotyc.ModelBinders;
using System.Linq.Dynamic.Core;
using System.Net;

namespace Sicotyc.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IRepositoryManager _repository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUploadFileService _uploadService;
        //private readonly IRepositoryStoreProcedure _repositoryStoreProcedure;
        
        public AuthenticationController(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAuthenticationManager authManager, 
            IRepositoryManager repository, IWebHostEnvironment hostingEnvironment,
            IUploadFileService uploadFileService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
            _uploadService = uploadFileService;
            //_repositoryStoreProcedure = repositoryStoreProcedure;            
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Autenticacion fallida. Nombre de Usuario o Contraseña incorrecto.");
                return Unauthorized("Autenticacion fallida. Nombre de Usuario o Contraseña incorrecto.");
            }   
            
            var userDB = await _userManager.FindByNameAsync(user.UserName);

            return Ok(new 
            { 
                Token = await _authManager.CreateTokenAsync(),
                Menu = await GetMenuItems(userDB.Id)
            });
        }

        [HttpPost("change-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        { 
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Puedes agregar aquí cualquier lógica adicional después de cambiar la contraseña
                // await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInfo($"Se cambio la contraseña para el usuario con el id: {model.Id}");
                return Ok("Contraseña cambiada exitosamente. ");
            }
            else
            {
                _logger.LogError($"Ocurrio un error con el usuario con id: {user.Id} al intentar cambiar su password");
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("token-reset-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> TokenResetPassword([FromBody] TokenResetPassword tokenResetPassword)
        { 
            var user = await _userManager.FindByIdAsync(tokenResetPassword.Id);
            if (user == null)
            {
                return BadRequest("Usuario no encontrado para generacion de token");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Almacena el token, el usuario y cualquier otra información necesaria en tu sistema
            // Por ejemplo, podrías almacenar esto en la base de datos o en una caché temporal            

            return Ok(new 
            { 
                Token = resetToken
            });

        }

        [HttpPost("reset-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            ResultProcess resultProcess = new ResultProcess();
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            
            if (result.Succeeded)
            {                
                resultProcess.Success = true;
                resultProcess.Message = $"La contraseña del usuario fue reseteada correctamente";
                resultProcess.Status = HttpStatusCode.OK;

                // Puedes agregar aquí cualquier lógica adicional después de restablecer la contraseña
                _logger.LogInfo($"La contraseña del usuario con id:{model.UserId} fue reseteada correctamente");
                return Ok(resultProcess);
            }
            else
            {
                resultProcess.Success = false;
                resultProcess.Message = $"Ocurrio un error al intentar resetear la contraseña para el usuario";
                resultProcess.Status = HttpStatusCode.BadRequest;
                _logger.LogError($"Ocurrio un error al intentar resetear la contraseña para el usuario con el id: {model.UserId}, aca el detalle: { result.Errors }");
                return BadRequest(resultProcess);
            }
        }

        [HttpGet("claims")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetClaims([FromQuery] string token) {
            try
            {                
                List<ClaimMetadata> claims = await GetClaimsAsync(token);

                if (claims.Count() > 0)
                    return Ok(new { Claims = claims });
                else
                    return NoContent(); // No contiene claims
            }
            catch (Exception ex)
            {
                _logger.LogError($"Se produjo un error al intentar leer el token {token}");
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("validateJWT")]
        public async Task<IActionResult> ValidateJWT() {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {    
                
                ResultProcess validToken = await _authManager.ValidateToken(tokenHeaderValue);

                if (validToken.Status == HttpStatusCode.OK)
                {
                    return Ok("Token valido");
                }
                else {
                    return Unauthorized(validToken.Message);
                }                                
            }

            return BadRequest("Token no valido");
        }

        [HttpGet("renewToken")]
        public async Task<IActionResult> RenewToken()
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Puedes trabajar con el valor del encabezado aquí
                string token = tokenHeaderValue.ToString();
                if (token == null)
                {
                    return BadRequest("No hay token en la peticion");
                }

                List<ClaimMetadata> claims = await _authManager.GetClaimsAsync(token);                

                if (claims.Count() > 0)
                {
                    string? uid = claims.Find(x => x.Type == "Id")?.Value;
                    var renewToken = _authManager.RenewTokenAsync(uid.ToString());                    

                    var userDto = _mapper.Map<UserDto>(renewToken.Result.User);
                    userDto.Roles = userDto.Roles = _userManager.GetRolesAsync(new User
                    {
                        Id = userDto.Id,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Email = userDto.Email,
                        UserName = userDto.UserName
                    }).Result.ToList();

                    Company company = await _repository.UserCompany.GetCompanyByUserIdAsync(uid, false);
                    userDto.Ruc = company.Ruc;

                    UserDetail userDetail = await _repository.UserDetail.GetUserDetailByUserIdAsync(uid, false);
                    userDto.UserDetail = userDetail;

                    return Ok(new { Token = renewToken.Result.Token,
                                    User = userDto,
                                    Roles = renewToken.Result.Roles,
                                    Menu = await GetMenuItems(uid)
                    });
                }
                return BadRequest("Token no valido");
            }
            else {
                return BadRequest("No hay token en la peticion");
            }
        }

        #region CRUD Users

        // Create
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration.Ruc == null || userForRegistration.Ruc.Length != 11)
            {
                return BadRequest("El ruc es un valor de 11 digitos numericos");
            }
            var user = _mapper.Map<User>(userForRegistration);
            if (userForRegistration.Password != null)
            {
                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                if (userForRegistration.Roles == null || userForRegistration.Roles?.Count() == 0) {

                    ICollection<string> roles = new List<string> { "Member" };
                    userForRegistration.Roles = roles;
                }               

                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

                #region Company
                // Company section
                var companyDB = await _repository.Company.GetCompanyByRucAsync(userForRegistration.Ruc, trackChanges: false);
                if (companyDB == null)
                {
                    // Register Company
                    Company company = new Company() 
                    { 
                        CompanyId = Guid.NewGuid(),
                        Ruc = userForRegistration.Ruc,
                        CreatedBy = user.Id
                    };
                    _repository.Company.CreateCompany(company);
                    await _repository.SaveAsync();


                    // Registramos en la tabla UserCompany
                    UserCompany userCompany = new UserCompany()
                    {
                        Id = user.Id,
                        CompanyId = company.CompanyId
                    };
                    _repository.UserCompany.CreateUserCompany(userCompany);
                    await _repository.SaveAsync();


                }
                else 
                {
                    // Registramos en la tabla UserCompany
                    UserCompany userCompany = new UserCompany()
                    {
                        Id = user.Id,
                        CompanyId = companyDB.CompanyId
                    };
                    _repository.UserCompany.CreateUserCompany(userCompany);
                    await _repository.SaveAsync();
                }

                #endregion

                #region User Detail
                // UserDetail section
                if (userForRegistration.UserDetail != null)
                {
                    var userDetailForCreationDto = _mapper.Map<UserDetailForCreationDto>(userForRegistration.UserDetail);
                    userDetailForCreationDto.Id = user.Id;
                    userDetailForCreationDto.CreatedBy = user.Id;
                    await _repository.SaveAsync();
                }

                #endregion

                return StatusCode(201); // 201 = Created

            }
            else {
                _logger.LogError($"No se puede registrar al usuario porque el password es nulo");
                return BadRequest(ModelState);
            }            
        }

        // Read
        [HttpGet("users")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
        {
            try
            {
                var usersFromDb = await _repository.AuthenticationManager.GetUsersAsync(userParameters, trackChanges: false);

                var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersFromDb);

                foreach (var userDto in usersDto)
                {
                    // Roles
                    userDto.Roles = _userManager.GetRolesAsync(new User
                    {
                        Id = userDto.Id,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Email = userDto.Email,
                        UserName = userDto.UserName
                    }).Result.ToList();

                    // Ruc
                    Company company = await _repository.UserCompany.GetCompanyByUserIdAsync(userDto.Id, false);
                    userDto.Ruc = company.Ruc;

                    // UserDetail
                    UserDetail userDetail = await _repository.UserDetail.GetUserDetailByUserIdAsync(userDto.Id, false);
                    userDto.UserDetail = userDetail;
                }

                return Ok(new
                {
                    data = usersDto,
                    pagination = usersFromDb.MetaData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda de usuarios, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda de usuarios");
            }
        }

        // Read Collection
        [HttpGet("users/collection({ids})")]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> GetUsersByIdCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids) {

            try
            {
                if (ids == null)
                {
                    _logger.LogError("Parametro ids es nulo");
                    return BadRequest("Parametro ids es nulo");
                }

                var usersFromDb = await _authManager.GetUsersByIdCollectionAsync(ids, trackChanges: false);
                if (ids.Count() != usersFromDb.Count())
                {
                    _logger.LogError("Algunos de los Ids de la coleccion no son validos");
                    return NotFound();
                }

                var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersFromDb);

                foreach (var userDto in usersDto)
                {
                    //Roles
                    userDto.Roles = _userManager.GetRolesAsync(new User
                    {
                        Id = userDto.Id,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Email = userDto.Email,
                        UserName = userDto.UserName
                    }).Result.ToList();

                    // Ruc
                    Company company = await _repository.UserCompany.GetCompanyByUserIdAsync(userDto.Id, trackChanges: false);
                    userDto.Ruc = company.Ruc;

                    // UserDetail
                    UserDetail userDetail = await _repository.UserDetail.GetUserDetailByUserIdAsync(userDto.Id, false);
                    userDto.UserDetail = userDetail;
                }

                return Ok(new { data = usersDto });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda de usuarios por coleccion, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda de usuarios por coleccion");
            }
        }

        // Read
        [HttpGet("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var userResult = await _userManager.FindByIdAsync(id.ToString());
                if (userResult == null)
                {
                    _logger.LogError($"Usuario con id: {id} no existe");
                    return NotFound();
                }
                else
                {
                    var userDto = _mapper.Map<UserDto>(userResult);
                    // Roles
                    userDto.Roles = _userManager.GetRolesAsync(new User
                    {
                        Id = userDto.Id,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Email = userDto.Email,
                        UserName = userDto.UserName
                    }).Result.ToList();

                    // Ruc
                    Company company = await _repository.UserCompany.GetCompanyByUserIdAsync(userDto.Id, false);
                    userDto.Ruc = company.Ruc;

                    // UserDetail
                    UserDetail userDetail = await _repository.UserDetail.GetUserDetailByUserIdAsync(userDto.Id, false);
                    userDto.UserDetail = userDetail;

                    return Ok(new { data = userDto });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda de usuario, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda de usuario");
            }
        }

        // Read
        [HttpGet("user/email/{email}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetUserByEmail(string email) {
            try
            {
                var userResult = await _userManager.FindByEmailAsync(email);
                if (userResult == null)
                {
                    _logger.LogError($"Usuario con email: {email} no existe");
                    return NotFound($"Usuario con email: {email} no existe");
                }
                else
                {
                    var userDto = _mapper.Map<UserDto>(userResult);
                    // Roles
                    userDto.Roles = _userManager.GetRolesAsync(new User
                    {
                        Id = userDto.Id,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Email = userDto.Email,
                        UserName = userDto.UserName
                    }).Result.ToList();

                    // Ruc
                    Company company = await _repository.UserCompany.GetCompanyByUserIdAsync(userDto.Id, false);
                    userDto.Ruc = company.Ruc;

                    // UserDetail
                    UserDetail userDetail = await _repository.UserDetail.GetUserDetailByUserIdAsync(userDto.Id, false);
                    userDto.UserDetail = userDetail;

                    return Ok(new { data = userDto });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de realizar la busqueda de usuario por email, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de realizar la busqueda de usuario por email");
            }
        }

        // Update
        [HttpPut("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto userDto)
        {
            try
            {
                var userDB = _userManager.FindByIdAsync(userDto.Id).Result;
                if (userDB == null)
                {
                    _logger.LogError($"Usuario con id: {userDto.Id} no existe");
                    return NotFound();
                }
                else
                {
                    //_mapper.Map(userDto, userDB); // No funciona porque no se puede excluir el Id

                    // Modificamos solo las propiedades necesarias
                    userDB.FirstName = userDto.FirstName != null ? userDto.FirstName : userDB.FirstName;
                    userDB.LastName = userDto.LastName != null ? userDto.LastName : userDB.LastName;
                    userDB.Email = userDto.Email != null ? userDto.Email : userDB.Email;
                    userDB.UserName = userDto.UserName != null ? userDto.UserName : userDB.UserName;
                    userDB.PhoneNumber = userDto.PhoneNumber != null ? userDto.PhoneNumber : userDB.PhoneNumber;

                    var result = await _userManager.UpdateAsync(userDB);
                    if (result.Succeeded)
                    {
                        // 1.- Delete all roles
                        var userDBRoles = _userManager.GetRolesAsync(userDB).Result;
                        await _userManager.RemoveFromRolesAsync(userDB, userDBRoles);

                        // 2.- Add the current roles
                        var currentUserRoles = userDto.Roles;
                        if (currentUserRoles != null)
                        {
                            await _userManager.AddToRolesAsync(userDB, currentUserRoles);
                        }

                        // Company y UserCompany
                        // 3.- Buscar el company por ruc
                        Company companyDB = await _repository.Company.GetCompanyByRucAsync(userDto.Ruc, false);
                        if (companyDB == null)
                        {
                            // 4.- Si no existe... registrar en la tabla company y luego en la tabla userCompany
                            companyDB = new Company()
                            {
                                CompanyId = Guid.NewGuid(),
                                Ruc = userDto.Ruc,
                                CreatedBy = userDto.Id
                            };
                            _repository.Company.CreateCompany(companyDB);
                            await _repository.SaveAsync();

                            // Eliminamos cualquier otro UserCompany asociado al Id del usuario
                            await _repository.UserCompany.DeleteAllCompaniesAssociatedUserAsync(userDto.Id, false);

                            // Insertamos el nuevo registro en la table UserCompany
                            UserCompany userCompany = new UserCompany()
                            {
                                Id = userDto.Id,
                                CompanyId = companyDB.CompanyId
                            };

                            _repository.UserCompany.CreateUserCompany(userCompany);
                            await _repository.SaveAsync();

                        }
                        else
                        {

                            // 5.- Si existe... obtener el id y actualizar en la tabla userCompany
                            // Eliminamos cualquier otro UserCompany asociado al Id del usuario
                            await _repository.UserCompany.DeleteAllCompaniesAssociatedUserAsync(userDto.Id, false);

                            // Insertamos el nuevo registro en la table UserCompany
                            UserCompany userCompany = new UserCompany()
                            {
                                Id = userDto.Id,
                                CompanyId = companyDB.CompanyId
                            };

                            _repository.UserCompany.CreateUserCompany(userCompany);
                            await _repository.SaveAsync();
                        }

                        #region UserDetail
                        UserDetail userDetailDB = await _repository.UserDetail.GetUserDetailByUserIdAsync(userDto.Id, true);
                        if (userDetailDB == null)
                        {
                            // Validamos si tenemos data de UserDetail para crear
                            if (userDto.UserDetail != null)
                            {
                                // Creamos data de UserDetail
                                //var userDetailForCreationDto = _mapper.Map<UserDetailForCreationDto>(userDto.UserDetail);
                                //userDetailForCreationDto.CreatedBy = userDto.Id;
                                var userDetailForCreationDto = new UserDetailForCreationDto();
                                _mapper.Map(userDto.UserDetail, userDetailForCreationDto);
                                userDetailForCreationDto.CreatedBy = id.ToString();

                                var userDetailDto = _mapper.Map<UserDetail>(userDetailForCreationDto);
                                _repository.UserDetail.CreateUserDetail(userDetailDto);
                                await _repository.SaveAsync();
                            }
                        }
                        else
                        {
                            // Validamos si tenemos data de UserDetail para actualizar
                            if (userDto.UserDetail != null)
                            {
                                var userDetailForUpdateDto = _mapper.Map<UserDetailForUpdateDto>(userDto.UserDetail);
                                userDetailForUpdateDto.UserDetailId = userDetailDB.UserDetailId;
                                userDetailForUpdateDto.UpdatedBy = userDto.Id;
                                userDetailForUpdateDto.LastUpdatedOn = DateTime.UtcNow;

                                _mapper.Map(userDetailForUpdateDto, userDetailDB);
                                await _repository.SaveAsync();
                            }
                        }

                        #endregion

                        _logger.LogInfo($"Se actualizo los datos del usuario con el id: {id}");
                        return Ok("Usuario actualizado satisfactoriamente");
                    }
                    else
                    {
                        _logger.LogError($"Hubo un error al intentar actualizar el usuario con id: {id}");
                        return BadRequest(result.Errors);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de actualizar al usuario con id: {id}, aca el detalle: {ex.Message}");
                return BadRequest($"Hubo un error al tratar de actualizar al usuario con id: {id}");
            }
        }

        // Delete        
        [HttpDelete("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var userDB = await _userManager.FindByIdAsync(id.ToString());

                if (userDB == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                // 1.- Delete all roles
                var userDBRoles = _userManager.GetRolesAsync(userDB).Result;
                await _userManager.RemoveFromRolesAsync(userDB, userDBRoles);

                // 2.- Delete image user
                if (userDB.Img != null)
                {
                    // Obtener la ruta fisica del directorio raiz del proyecto
                    string rootPath = _hostingEnvironment.ContentRootPath;
                    await this._uploadService.DeleteImageAsync("USERS", rootPath, userDB.Img);

                }

                // 3.- Delete from userCompany
                var companyId = await _repository.UserCompany.GetCompanyIdByUserIdAsync(userDB.Id, false);
                if (companyId != null)
                {
                    UserCompany userCompany = new UserCompany()
                    {
                        Id = userDB.Id,
                        CompanyId = companyId,
                    };

                    _repository.UserCompany.DeleteUserCompany(userCompany);
                    await _repository.SaveAsync();
                }

                // 4.- Delete user
                var result = await _userManager.DeleteAsync(userDB);

                if (result.Succeeded)
                {

                    _logger.LogInfo($"Usuario con el id: {id} fue eliminado exitosamente");
                    return Ok("Usuario eliminado exitosamente");
                }
                else
                {
                    _logger.LogError($"Hubo un error al intentar eliminar al usuario con el id: {id}, aca el detalle: {result.Errors}");
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de eliminar al usuario con id: {id}, aca el detalle: {ex.Message}");
                return BadRequest($"Hubo un error al tratar de eliminar al usuario con id: {id}");
            }
        }

        #endregion

        #region UserDetails
        [HttpGet("userDetails/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationTokenFilter))]
        private async Task<IActionResult> GetUserDetails(Guid id) 
        {
            try
            {
                UserDetail userDetail = await _repository.UserDetail.GetUserDetailByUserIdAsync(id.ToString(), false);
                // TODO: Revisar si se necesita mapper

                if (userDetail == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(new { data = userDetail });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de obtener el detalle del usuario, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de obtener el detalle del usuario");
            }

        }
        #endregion

        #region Menu options
        [HttpGet("menu-options/user/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetMenuByUserId(string id)
        {
            try
            {               
                return Ok(new {
                    userId = id,
                    menu = await GetMenuItems(id)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hubo un error al tratar de obtener la lista de opciones de menu para el usuario {id}, aca el detalle: {ex.Message}");
                return BadRequest("Hubo un error al tratar de obtener la lista de opciones de menu para el usuario");
            }
        }
        #endregion

        #region Private methods
        private async Task<List<ClaimMetadata>> GetClaimsAsync(string token) { 
        
            var claims = new List<ClaimMetadata>();

            try
            {
                // Aplicamos una restriccion de que Claims podemos devolver.
                LookupCodeParameters lookupCodeParameters = new LookupCodeParameters();

                var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupByNameAsync(LookupCodeGroupEnum.CLAIMS_PERMITIDOS.GetStringValue(), trackChanges: false);
                if (lookupCodeGroup != null)
                {
                    var lookupCodesFromDb = await _repository.LookupCode.GetLookupCodesAsync(lookupCodeGroup.Id, lookupCodeParameters, trackChanges: false);
                    var lookupCodesDto = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodesFromDb);
                    var claimsResult = await _authManager.GetClaimsAsync(token);
                    if (claimsResult.Count() > 0) 
                    {
                        List<ClaimMetadata> result = new List<ClaimMetadata>();
                        // Evaluamos para ver que Claims enviamos
                        foreach (var item in claimsResult) {
                            if (lookupCodesDto.Any(c => c.LookupCodeValue?.Trim().ToLower() == item.Type?.Trim().ToLower()))
                            {
                                result.Add(item);
                            }
                        }
                        claims = result;
                    }
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Se produjo un error al intentar leer el token {token}, para mayor detalles: {ex.Message}");
                throw;
            }

            return claims;
        }

        private async Task<List<MenuItem>> GetMenuItems(string id) {
            List<MenuItem> menu = new List<MenuItem>();           

            try
            {
                // 1.- Validamos si el usuario existe
                var userResult = await _userManager.FindByIdAsync(id.ToString());
                if (userResult == null)
                {
                    _logger.LogError($"Usuario con id: {id} no existe");                    
                }

                // 2.- Obtenemos los roles del usuario
                var rolesUser = _userManager.GetRolesAsync(userResult).Result.ToList();
                if (rolesUser == null)
                {
                    _logger.LogError($"Usuario con id: {id} no tiene roles");                    
                }

                // 3.- Aca traemos las opciones necesarias buscando en tablas SQL (TODO)
                var itemsMenu = await _repository.RepositoryStoreProcedure.GetMenuOptionsByRoleAsync("Administrator");

                if (!itemsMenu.Any(o => o.IsEnabled.Equals(true)))
                {
                    #region Opcion de menu por defecto
                    MenuItem menuItem = new MenuItem();
                    menuItem.Title = "Dashboard";
                    menuItem.Icon = "mdi mdi-gauge";

                    List<SubMenuItem> subMenuItems = new List<SubMenuItem>();
                    subMenuItems.Add(new SubMenuItem { Title = "Main", Url = "/" });
                    subMenuItems.Add(new SubMenuItem { Title = "ProgressBar", Url = "/dashboard/progress" });
                    subMenuItems.Add(new SubMenuItem { Title = "Graficas", Url = "/dashboard/grafica1" });
                    subMenuItems.Add(new SubMenuItem { Title = "Promesas", Url = "/dashboard/promesas" });
                    subMenuItems.Add(new SubMenuItem { Title = "Juegos Azar", Url = "/dashboard/juegos-azar" });
                    subMenuItems.Add(new SubMenuItem { Title = "rxJS", Url = "/dashboard/rxjs" });
                    menuItem.submenu = subMenuItems;

                    menu.Add(menuItem);

                    #endregion
                }
                else
                {
                    List<OptionByRole> parentOptions = itemsMenu.FindAll(o => o.IsEnabled && o.OptionLevel == 1)
                                                                .OrderBy(o => o.OptionOrder).ToList();
                    parentOptions.ForEach(o =>
                    {
                        if (itemsMenu.Any(c => c.OptionParentId == o.OptionId && c.IsEnabled && c.OptionLevel == 2)) { 
                            // Tiene hijos
                            // Insertamos los Parents
                            MenuItem menuItem = new MenuItem();
                            menuItem.Title = o.Title;
                            menuItem.Icon = o.Icon;

                            List<SubMenuItem> subMenuItems = new List<SubMenuItem>();
                            List<OptionByRole> chilOptions = itemsMenu.FindAll(c => c.OptionParentId == o.OptionId && c.IsEnabled && c.OptionLevel == 2)
                                                                        .OrderBy(o => o.OptionOrder).ToList();
                            chilOptions.ForEach(c =>
                            {
                                subMenuItems.Add(new SubMenuItem { Title = c.Title, Url = c.Url, });
                            });
                            menuItem.submenu = subMenuItems;
                            menu.Add(menuItem);
                        }
                    });
                }
            }
            catch (Exception ex )
            {
                _logger.LogError($"Hubo un error al tratar de obtener la lista de opciones de menu para el usuario {id}, aca el detalle: {ex.Message}");
                throw;
            }

            // 4.- Retornamos el menu en el formato que necesita la aplicacion front-end
            return menu;
        }
        #endregion
    }
}
