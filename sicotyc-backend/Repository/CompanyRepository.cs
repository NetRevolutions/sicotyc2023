using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {            
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id, bool trackChanges) =>
            await FindByCondition(o => o.CompanyId.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<Company> GetCompanyByRucAsync(string ruc, bool trackChanges) =>
            await FindByCondition(o => o.Ruc.Equals(ruc), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCompany(Company company) => Create(company);

        public void DeleteCompany(Company company) => Delete(company);

        public async void DeleteCompanyByRuc(string ruc)
        {
            Company company = await GetCompanyByRucAsync(ruc, false);
            if (company != null)
                DeleteCompany(company);
        }

        
    }
}
