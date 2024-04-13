using Entities.Models;

namespace Contracts
{
    public interface IRepositoryStoreProcedure
    {
        Task<List<OptionByRole>> GetMenuOptionsByRoleAsync(string roleName);
    }
}
