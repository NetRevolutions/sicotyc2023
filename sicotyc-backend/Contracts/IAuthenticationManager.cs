using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken(); 
        Task<PagedList<User>> GetUsersAsync(UserParameters userParameters, bool trackChanges);

    }
}
