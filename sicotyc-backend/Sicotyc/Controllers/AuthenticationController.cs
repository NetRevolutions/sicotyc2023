using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sicotyc.ActionFilters;
using Sicotyc.ModelBinders;

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

        [HttpGet("users")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetUsers([FromQuery]UserParameters userParameters)
        {
            var usersFromDb = await _respository.AuthenticationManager.GetUsersAsync(userParameters, trackChanges: false);                    

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(usersFromDb.MetaData));

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersFromDb);

            foreach (var userDto in usersDto) {
                userDto.Roles = _userManager.GetRolesAsync(new User { 
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    UserName = userDto.UserName
                }).Result.ToList();
            }

            return Ok(usersDto);
        }

        [HttpGet("user/{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var userResult = _userManager.FindByIdAsync(id.ToString()).Result;
            if (userResult == null)
            {
                _logger.LogError($"Usuario con id: {id} no existe");
                return NotFound();
            }
            else
            { 
                var userDto = _mapper.Map<UserDto>(userResult);
                userDto.Roles = _userManager.GetRolesAsync(new User { 
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                    UserName = userDto.UserName
                }).Result.ToList();

                return Ok(userDto);
            }
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

            return Ok(new { Token = await _authManager.CreateToken() });
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        { 
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return StatusCode(201); // 201 = Created
        }
    }
}
