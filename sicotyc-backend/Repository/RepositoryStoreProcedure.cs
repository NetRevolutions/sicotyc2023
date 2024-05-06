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

        public async Task<List<MenuOptionRole>> UpdateOptionRoleAsync(string roleName, string optionIds)
        {
            return await _repositoryContext.MenuOptionRoles.FromSqlRaw("EXEC [SCT].[USP_ASSIGN_MENU_OPTIONS] @p0, @p1", roleName, optionIds).ToListAsync();
        }
    }
}
