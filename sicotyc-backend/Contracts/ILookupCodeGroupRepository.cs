using Entities.Models;

namespace Contracts
{
    public interface ILookupCodeGroupRepository
    {
        IEnumerable<LookupCodeGroup> GetAllLookupCodeGroups(bool trackChanges);
        LookupCodeGroup GetLookupCodeGroup(Guid lookupCodeGroupId, bool trackChanges);
    }
}
