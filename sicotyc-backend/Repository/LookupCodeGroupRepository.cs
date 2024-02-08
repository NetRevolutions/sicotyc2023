using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LookupCodeGroupRepository : RepositoryBase<LookupCodeGroup>, ILookupCodeGroupRepository
    {
        public LookupCodeGroupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {            
        }

        public async Task<IEnumerable<LookupCodeGroup>> GetAllLookupCodeGroupsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(x => x.Name)
            .ToListAsync();

        public async Task<LookupCodeGroup> GetLookupCodeGroupAsync(Guid lookupCodeGroupId, bool trackChanges) => 
            await FindByCondition(l => l.Id.Equals(lookupCodeGroupId), trackChanges)            
            .SingleOrDefaultAsync();

        public async Task<LookupCodeGroup> GetLookupCodeGroupByNameAsync(string name, bool trackChanges) =>        
            await FindByCondition(l => l.Name.Trim().ToLower().Equals(name.Trim().ToLower()), trackChanges)
                .SingleOrDefaultAsync();
        

        public async Task<IEnumerable<LookupCodeGroup>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public void CreateLookupCodeGroup(LookupCodeGroup lookupCodeGroup) => Create(lookupCodeGroup);
                
        public void DeleteLookupCodeGroup(LookupCodeGroup lookupCodeGroup)
        {
            Delete(lookupCodeGroup);
        }

        
    }
}
