using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ILookupCodeGroupService
    {
        IEnumerable<LookupCodeGroupDto> GetAllLookupCodeGroups(bool trackChanges);
        LookupCodeGroupDto GetLookupCodeGroup(Guid lookupCodeGroupId, bool trackChanges);
    }
}
