using Entities.Models;

namespace Contracts
{
    public interface IUserCompanyRepository
    {
        Task<bool> ExistUserCompanyAsync(UserCompany userCompany, bool trackChanges);
        Task<List<string>> GetUserIdsByCompanyId(Guid companyId, bool trackChanges);
        Task<Guid> GetCompanyIdByUserIdAsync(string userId, bool trackChanges);
        Task<string> GetUserIdByCompanyIdAsync(Guid companyId, bool trackChanges);
        Task<Company> GetCompanyByUserIdAsync(string userId, bool trackChanges);
       
        void CreateUserCompany(UserCompany userCompany);
        void DeleteUserCompany(UserCompany userCompany);
        Task<List<Company>> GetAllCompanyIdsByUserIdAsync(string userId, bool trackChanges);
        Task DeleteAllCompaniesAssociatedUserAsync(string userId, bool trackChanges);

    }
}
