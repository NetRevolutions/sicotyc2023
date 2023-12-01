using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LookupCodeRepository : RepositoryBase<LookupCode>, ILookupCodeRepository
    {
        public LookupCodeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public IEnumerable<LookupCode> GetLookupCodes(Guid lookupCodeGroupId, bool trackchanges) =>
            FindByCondition(lc => lc.LookupCodeGroupId.Equals(lookupCodeGroupId), trackchanges)
            .OrderBy(lc => lc.LookupCodeOrder)
            .ToList();
        public LookupCode GetLookupCode(Guid lookupCodeGroupId, Guid id, bool trackChanges) =>
            FindByCondition(lc => lookupCodeGroupId.Equals(lookupCodeGroupId) && lc.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId, LookupCode lookupCode)
        {
            lookupCode.LookupCodeGroupId = lookupCodeGroupId;
            Create(lookupCode);
        }

        public void DeleteLookupCode(LookupCode lookupCode)
        {
            Delete(lookupCode);
        }

        public int GetLastLookupCodeOrder(Guid lookupCodeGroupId) =>            
            FindByCondition(lc => lc.LookupCodeGroupId.Equals(lookupCodeGroupId), false)
            .Where(w => !w.DeleteDtm.HasValue)
            .OrderByDescending(o => o.LookupCodeOrder)
            .Select(s => s.LookupCodeOrder)
            .FirstOrDefault();

        public IEnumerable<LookupCode> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();
            
    }
}
