using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryStoreProcedure : IRepositoryStoreProcedure
    {
        private readonly RepositoryContext _repositoryContext;

        public RepositoryStoreProcedure(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task<List<OptionByRole>> GetMenuOptionsByRoleAsync(string roleName)
        {
            
            return await _repositoryContext.OptionByRoles.FromSqlRaw("EXEC [SCT].[USP_GET_MENU_OPTIONS_BY_ROLE] @p0", roleName).ToListAsync();
        }
    }
}
