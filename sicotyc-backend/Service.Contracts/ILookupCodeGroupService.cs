using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;


namespace Service.Contracts
{
    public interface ILookupCodeGroupService
    {
        Task<IEnumerable<LookupCodeGroupDto>> GetAllLookupCodeGroupsAsync(LookupCodeGroupParameters lookupCodeGroupParameters, bool trackChanges);
        Task<LookupCodeGroupDto> GetLookupCodeGroupAsync(Guid lookupCodeGroupId, bool trackChanges);
    }
}
