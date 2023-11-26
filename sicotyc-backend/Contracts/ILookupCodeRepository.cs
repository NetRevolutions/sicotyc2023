using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILookupCodeRepository
    {
        IEnumerable<LookupCode> GetLookupCodes(Guid lookupCodeGroupId, bool trackchanges);
        LookupCode GetLookupCode(Guid lookupCodeGroupId, Guid Id, bool trackChanges);
    }
}
