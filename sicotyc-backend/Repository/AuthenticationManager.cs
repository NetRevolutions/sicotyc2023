using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Repository
{
    public class AuthenticationManager : RepositoryBase<User>, IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;       

        private User? _user;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration, RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _userManager = userManager;
            _configuration = configuration;  
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);

            return _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password);
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<RenewToken> RenewTokenAsync(string userId)
        {
            _user = await _userManager.FindByIdAsync(userId);                       

            // Obtener los roles por usuario
            var userRolesDB = await _userManager.GetRolesAsync(_user);
            List<string> roles = new List<string>();
            foreach (var role in userRolesDB)
            {
                roles.Add(role.ToString());
            }

            var token = await CreateTokenAsync();

            return new RenewToken { 
                Token = token,
                User = _user,
                Roles = roles
            };

        }

        public async Task<List<User>> GetUsersByIdCollectionAsync(IEnumerable<string> ids, bool trackChanges)
        {
            var users = new List<User>();
            if (ids.Count() > 0)
            {
                users = await FindAll(trackChanges)
                    .Where(u => ids.Contains(u.Id))
                    .OrderBy(o => o.FirstName)
                    .ToListAsync();
            }

            return users;
        }

        public async Task<PagedList<User>> GetUsersAsync(UserParameters userParameters, bool trackChanges)
        {
            var users = new List<User>();
            if (!String.IsNullOrEmpty(userParameters.SearchTerm))
            {
                //users = await FindByCondition(u => u.UserName.Contains(userParameters.SearchTerm), trackChanges)                
                //.OrderBy(o => o.FirstName)
                //.ToListAsync();

                users = await FindAll(trackChanges)
                .Where(u => u.UserName.Contains(userParameters.SearchTerm) ||
                            u.FirstName.Contains(userParameters.SearchTerm) ||
                            u.LastName.Contains(userParameters.SearchTerm) ||
                            u.Email.Contains(userParameters.SearchTerm))
                .OrderBy(o => o.FirstName)
                .ToListAsync();
            }
            else
            {
                users = await FindAll(trackChanges)                
                    .OrderBy(o => o.FirstName)
                    .ToListAsync();
            }

            // TODO: Pendiente traer los roles de cada usuario


            return PagedList<User>
                .ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);
                
        }        

        public async Task<List<ClaimMetadata>> GetClaimsAsync(string token)
        {            
            List<ClaimMetadata> claimList = new List<ClaimMetadata>();
            try
            {
                // Decodificar el token
                var claimsPrincipal = await getTokenValidationResult(token); 

                if (claimsPrincipal.IsValid)
                {
                    var claims = claimsPrincipal.Claims;
                    foreach ( var claim in claims)
                    {                        
                        claimList.Add(new ClaimMetadata { Type = claim.Key, Value = claim.Value.ToString() });
                    }                    
                }

                return claimList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultProcess> ValidateToken(string token)
        {
            ResultProcess resultProcess = new ResultProcess();
            resultProcess.Success = true;
            resultProcess.Status = HttpStatusCode.OK;

            if (token == null)
            {
                resultProcess.Success = false;
                resultProcess.Message = "Token no valido";
                resultProcess.Status = HttpStatusCode.Unauthorized;
                return resultProcess;
            }

            List<ClaimMetadata> claims = await GetClaimsAsync(token);
            if (claims == null || claims.Count == 0)
            {
                resultProcess.Success = false;
                resultProcess.Message = "Token no valido";
                resultProcess.Status = HttpStatusCode.Unauthorized;
                return resultProcess;
            }            


            var tokenValidationResult = await getTokenValidationResult(token);

            var expirationDate = tokenValidationResult.SecurityToken.ValidTo.ToUniversalTime();
            if (expirationDate < DateTime.UtcNow)
            {
                resultProcess.Success = false;
                resultProcess.Message = "Token expirado";
                resultProcess.Status = HttpStatusCode.Unauthorized;
                return resultProcess;
            }

            return resultProcess;
        }        

        #region Private Methods

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            { 
                //new Claim(ClaimTypes.Name, _user.UserName),
                new Claim("UserName", _user?.UserName != null ? _user.UserName : string.Empty),
                new Claim("FirstName", _user?.FirstName != null ? _user.FirstName : string.Empty),
                new Claim("LastName", _user?.LastName != null ? _user.LastName : string.Empty),
                new Claim("Email", _user?.Email != null ? _user.Email : string.Empty),
                new Claim("Id", _user?.Id != null ? _user.Id : string.Empty),
                new Claim("Img", _user?.Img != null ? _user.Img : string.Empty),
                new Claim("PhoneNumber", _user?.PhoneNumber != null ? _user.PhoneNumber : string.Empty)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                //claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("Role", role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
                (
                    issuer: jwtSettings.GetSection("validIssuer").Value,
                    audience: jwtSettings.GetSection("validAudience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                    signingCredentials: signingCredentials
                );
            return tokenOptions;
        }

        private async Task<TokenValidationResult> getTokenValidationResult(string token)
        {
            // Configurar la validación del token
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            // Decodificar el token
            return await tokenHandler.ValidateTokenAsync(token, validationParameters);
        }        

        #endregion Private Methods

    }
}
