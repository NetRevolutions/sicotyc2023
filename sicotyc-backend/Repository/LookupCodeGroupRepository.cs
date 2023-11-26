using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LookupCodeGroupRepository : RepositoryBase<LookupCodeGroup>, ILookupCodeGroupRepository
    {
        public LookupCodeGroupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {            
        }

        public IEnumerable<LookupCodeGroup> GetAllLookupCodeGroups(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Name)
            .ToList();
    }
}
