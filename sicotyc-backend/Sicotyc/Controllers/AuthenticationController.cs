using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sicotyc.ActionFilters;

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
        private readonly IRepositoryManager _respository;
        public AuthenticationController(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAuthenticationManager authManager, IRepositoryManager repository)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _respository = repository;

        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Autenticacion fallida. Nombre de Usuario o Contraseña incorrecto.");
                return Unauthorized();
            }

            //var userDB = await _userManager.FindByNameAsync(user.UserName);

            return Ok(new { Token = await _authManager.CreateToken() });
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

        #region CRUD Users

        // Create
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
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
            var usersFromDb = await _respository.AuthenticationManager.GetUsersAsync(userParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(usersFromDb.MetaData));

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersFromDb);

            foreach (var userDto in usersDto)
            {
                userDto.Roles = _userManager.GetRolesAsync(new User
                {
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    UserName = userDto.UserName
                }).Result.ToList();
            }

            return Ok(usersDto);
        }

        // Read
        [HttpGet("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetUser(Guid id)
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
                userDto.Roles = _userManager.GetRolesAsync(new User
                {
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    UserName = userDto.UserName
                }).Result.ToList();

                return Ok(userDto);
            }
        }

        // Update
        [HttpPut("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto userDto)
        {            
            var userDB = _userManager.FindByIdAsync(id.ToString()).Result;
            if (userDB == null)
            {
                _logger.LogError($"Usuario con id: {id} no existe");
                return NotFound();
            }
            else {

                //_mapper.Map(userDto, userDB); // No funciona porque no se puede excluir el Id

                // Modificamos solo las propiedades necesarias
                userDB.FirstName = userDto.FirstName;
                userDB.LastName = userDto.LastName;
                userDB.Email = userDto.Email;
                userDB.UserName = userDto.UserName;
                userDB.PhoneNumber = userDto.PhoneNumber;                

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
                    _logger.LogInfo($"Se actualizo los datos del usuario con el id: {id}");
                    return Ok("Usuario actualizado satisfactoriamente");
                }
                else {
                    _logger.LogError($"Hubo un error al intentar actualizar el usuario con id: {id}");
                    return BadRequest(result.Errors);
                }
            }
        }

        // Delete        
        [HttpDelete("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {

                _logger.LogInfo($"Usuario con el id: {id} fue eliminado exitosamente");
                return Ok("Usuario eliminado exitosamente");
            }
            else
            {
                _logger.LogError($"Hubo un error al intentar eliminar al usuario con el id: {id}");
                return BadRequest(result.Errors);
            }
        }

        #endregion
    }
}
