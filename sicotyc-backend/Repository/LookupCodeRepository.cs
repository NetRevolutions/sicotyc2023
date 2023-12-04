using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class LookupCodeRepository : RepositoryBase<LookupCode>, ILookupCodeRepository
    {
        public LookupCodeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public async Task<IEnumerable<LookupCode>> GetLookupCodesAsync(Guid lookupCodeGroupId, bool trackchanges) =>
            await FindByCondition(lc => lc.LookupCodeGroupId.Equals(lookupCodeGroupId), trackchanges)
            .OrderBy(lc => lc.LookupCodeOrder)
            .ToListAsync();

        public async Task<LookupCode> GetLookupCodeAsync(Guid lookupCodeGroupId, Guid id, bool trackChanges) =>
            await FindByCondition(lc => lookupCodeGroupId.Equals(lookupCodeGroupId) && lc.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<int> GetLastLookupCodeOrderAsync(Guid lookupCodeGroupId) =>            
            await FindByCondition(lc => lc.LookupCodeGroupId.Equals(lookupCodeGroupId), false)
            .Where(w => !w.DeleteDtm.HasValue)
            .OrderByDescending(o => o.LookupCodeOrder)
            .Select(s => s.LookupCodeOrder)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<LookupCode>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public void CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, LookupCode lookupCode)
        {
            lookupCode.LookupCodeGroupId = lookupCodeGroupId;
            Create(lookupCode);
        }

        public void DeleteLookupCode(LookupCode lookupCode)
        {
            Delete(lookupCode);
        }

            
    }
}
