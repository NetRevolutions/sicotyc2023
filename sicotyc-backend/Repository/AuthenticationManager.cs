using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AuthenticationManager : RepositoryBase<User>, IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User _user;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration, RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);

            return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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
                
               
            return PagedList<User>
                .ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);
                
        }

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
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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

    }
}
