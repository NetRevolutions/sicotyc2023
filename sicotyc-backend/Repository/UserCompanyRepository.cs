using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserCompanyRepository : RepositoryBase<UserCompany>, IUserCompanyRepository
    {
        public UserCompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {            
        }

        public async Task<bool> ExistUserCompanyAsync(UserCompany userCompany, bool trackChanges)
        {
            return await FindByCondition(o => o.Id!.Equals(userCompany.Id) && o.CompanyId.Equals(userCompany.CompanyId), trackChanges)
                .AnyAsync();            
        }

        public async Task<Guid> GetCompanyIdByUserIdAsync(string userId, bool trackChanges)
        {
            return await FindByCondition(o => o.Id.Equals(userId), trackChanges)
                .Select(s => s.CompanyId)
                .FirstOrDefaultAsync();
                
        }

        public async Task<string> GetUserIdByCompanyIdAsync(Guid companyId, bool trackChanges)
        {
            return await FindByCondition(o => o.CompanyId.Equals(companyId), trackChanges)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Company> GetCompanyByUserIdAsync(string userId, bool trackChanges)
        {
            return await FindByCondition(o => o.Id.Equals(userId), trackChanges)
                .Select(s => s.Company)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Company>> GetAllCompanyIdsByUserIdAsync(string userId, bool trackChanges)
        {
            return await FindByCondition(o => o.Id.Equals(userId), trackChanges)
                .Select(s => s.Company)
                .ToListAsync();
        }

        public void CreateUserCompany(UserCompany userCompany) => Create(userCompany);

        public void DeleteUserCompany(UserCompany userCompany) => Delete(userCompany);

        public async Task DeleteAllCompaniesAssociatedUserAsync(string userId, bool trackChanges)
        { 
            List<Company> companies = await GetAllCompanyIdsByUserIdAsync(userId, trackChanges);
            foreach (var company in companies)
            {
                UserCompany uc = new UserCompany() { 
                    Id = userId,
                    CompanyId = company.CompanyId
                };

                DeleteUserCompany(uc);
            }
        }

        public async Task<List<string>> GetUserIdsByCompanyId(Guid companyId, bool trackChanges)
        {
           return await FindByCondition(o => o.CompanyId.Equals(companyId), trackChanges)
                .Select(s => s.Id)
                .ToListAsync();
        }
    }
}
