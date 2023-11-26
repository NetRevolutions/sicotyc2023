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
        public LookupCode GetLookupCode(Guid lookupCodeGroupId, Guid Id, bool trackChanges) =>
            FindByCondition(lc => lookupCodeGroupId.Equals(lookupCodeGroupId) && lc.Id.Equals(Id), trackChanges)
            .SingleOrDefault();
    }
}
