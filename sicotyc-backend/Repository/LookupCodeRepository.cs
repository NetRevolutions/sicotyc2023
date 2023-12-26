using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class LookupCodeRepository : RepositoryBase<LookupCode>, ILookupCodeRepository
    {
        public LookupCodeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public async Task<PagedList<LookupCode>> GetLookupCodesAsync(Guid lookupCodeGroupId, LookupCodeParameters lookupCodeParameters, bool trackChanges)
        {            
            var lookupCodes = await FindByCondition(e => e.LookupCodeGroupId.Equals(lookupCodeGroupId), trackChanges)
                .Search(lookupCodeParameters.SearchTerm)
                .Sort(lookupCodeParameters.OrderBy)
                //.OrderBy(lc => lc.LookupCodeOrder)
                //.Skip((lookupCodeParameters.PageNumber -1) * lookupCodeParameters.PageSize)
                //.Take(lookupCodeParameters.PageSize)
                .ToListAsync();

            //var count = await FindByCondition(e => e.LookupCodeGroupId.Equals(lookupCodeGroupId), trackChanges).CountAsync();

            //return new PagedList<LookupCode>(lookupCodes, lookupCodeParameters.PageNumber, lookupCodeParameters.PageSize, count);
            return PagedList<LookupCode>
                .ToPagedList(lookupCodes, lookupCodeParameters.PageNumber, lookupCodeParameters.PageSize);
        }           

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
