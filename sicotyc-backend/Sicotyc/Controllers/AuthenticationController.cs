using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Enum;
using Entities.Models;
using Entities.RequestFeatures;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(new { Token = await _authManager.CreateTokenAsync()/*, CompanyId = companyId*/ });
        }

        [HttpPost("change-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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
                return Ok("Contraseña cambiada exitosamente.");
            }
            else
            {
                _logger.LogError($"Ocurrio un error con el usuario con id: {user.Id} al intentar cambiar su password");
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("token-reset-password/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> TokenResetPassword(Guid id)
        { 
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return BadRequest("Usuario no encontrado para generacion de token");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Almacena el token, el usuario y cualquier otra información necesaria en tu sistema
            // Por ejemplo, podrías almacenar esto en la base de datos o en una caché temporal

            return Ok(new { Token = resetToken });

        }

        [HttpPost("reset-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                // Puedes agregar aquí cualquier lógica adicional después de restablecer la contraseña
                _logger.LogInfo($"La contraseña del usuario con id:{model.UserId} fue reseteada correctamente");
                return Ok("Contraseña restablecida exitosamente.");
            }
            else
            {
                _logger.LogError($"Ocurrio un error al intentar resetear la contraseña para el usuario con el id: {model.UserId}");
                return BadRequest(result.Errors);
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

                    return Ok(new { Token = renewToken.Result.Token,
                                    User = userDto,
                                    Roles = renewToken.Result.Roles
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
        public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }                

                try
                {
                    var usersFromDb = await _repository.AuthenticationManager.GetUsersAsync(userParameters, trackChanges: false);

                    //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(usersFromDb.MetaData));

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
            else
            {
                return BadRequest("No existe token para realizar esta accion");
            }            
        }

        // Read Collection
        [HttpGet("users/collection({ids})")]
        public async Task<IActionResult> GetUsersByIdCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids) {

            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue)) 
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }

                try
                {
                    if (ids == null)
                    {
                        _logger.LogError("Parametro ids es nulo");
                        return BadRequest("Parametro ids es nulo");
                    }

                    var userEntities = await _authManager.GetUsersByIdCollectionAsync(ids, trackChanges: false);
                    if (ids.Count() != userEntities.Count())
                    {
                        _logger.LogError("Algunos de los Ids de la coleccion no son validos");
                        return NotFound();
                    }

                    var usersToReturn = _mapper.Map<IEnumerable<UserDto>>(userEntities);                    

                    usersToReturn.ForEach(async user =>
                    {
                        var userTemp = userEntities.Find(x =>  x.Id == user.Id);
                        if (userTemp != null)
                        {
                            user.Roles = _userManager.GetRolesAsync(userTemp).Result;
                        }

                        // Ruc
                        Company company = await _repository.UserCompany.GetCompanyByUserIdAsync(user.Id, false);
                        user.Ruc = company.Ruc;
                    });
                    

                    return Ok(new { data = usersToReturn });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Hubo un error al tratar de realizar la busqueda de usuarios por coleccion, aca el detalle: {ex.Message}");
                    return BadRequest("Hubo un error al tratar de realizar la busqueda de usuarios por coleccion");
                }
            }
            else
            {
                return BadRequest("No existe token para realizar esta accion");
            }            
        }

        // Read
        [HttpGet("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetUser(Guid id)
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }

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

                        return Ok(new { data =  userDto });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Hubo un error al tratar de realizar la busqueda de usuario, aca el detalle: {ex.Message}");
                    return BadRequest("Hubo un error al tratar de realizar la busqueda de usuario");
                }
            }
            else
            {
                return BadRequest("No existe token para realizar esta accion");
            }            
        }

        // Update
        [HttpPut("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto userDto)
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }

                try
                {
                    var userDB = _userManager.FindByIdAsync(userDto.Id).Result;
                    if (userDB == null)
                    {
                        _logger.LogError($"Usuario con id: {id} no existe");
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
                                companyDB = new Company() {
                                    CompanyId = Guid.NewGuid(),
                                    Ruc = userDto.Ruc,
                                    CreatedBy = userDto.Id
                                };
                                _repository.Company.CreateCompany(companyDB);
                                await _repository.SaveAsync();

                                // Eliminamos cualquier otro UserCompany asociado al Id del usuario
                                await _repository.UserCompany.DeleteAllCompaniesAssociatedUserAsync(userDto.Id, false);

                                // Insertamos el nuevo registro en la table UserCompany
                                UserCompany userCompany = new UserCompany() { 
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
            else
            {
                return BadRequest("No hay token en la peticion");
            }            
        }

        // Delete        
        [HttpDelete("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // Acceder al encabezado "x-token" desde HttpContext
            if (HttpContext.Request.Headers.TryGetValue("x-token", out var tokenHeaderValue))
            {
                // Implementamos validacion del token
                var resultValidateToken = _authManager.ValidateToken(tokenHeaderValue).Result;
                if (!resultValidateToken.Success)
                {
                    return Unauthorized(resultValidateToken.Message);
                }

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
                        UserCompany userCompany = new UserCompany() { 
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
            else
            {
                return BadRequest("No hay token en la peticion");
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
        #endregion
    }
}
