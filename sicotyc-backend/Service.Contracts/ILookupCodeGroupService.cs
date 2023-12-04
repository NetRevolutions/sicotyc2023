using Entities.DataTransferObjects;
using Entities.Models;


namespace Service.Contracts
{
    public interface ILookupCodeGroupService
    {
        Task<IEnumerable<LookupCodeGroupDto>> GetAllLookupCodeGroupsAsync(bool trackChanges);
        Task<LookupCodeGroupDto> GetLookupCodeGroupAsync(Guid lookupCodeGroupId, bool trackChanges);
    }
}
