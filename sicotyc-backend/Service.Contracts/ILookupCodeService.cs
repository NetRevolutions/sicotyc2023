using Entities.DataTransferObjects;

namespace Service.Contracts
{
    public interface ILookupCodeService
    {
        Task<IEnumerable<LookupCodeDto>> GetLookupCodesAsync(Guid lookupCodeGroupId, bool trackChanges);
        Task<LookupCodeDto> GetLookupCodeAsync(Guid lookupCodeGroupId, Guid id, bool trackChanges);        
    }
}
