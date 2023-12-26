using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Service.Contracts
{
    public interface ILookupCodeService
    {
        Task<IEnumerable<LookupCodeDto>> GetLookupCodesAsync(Guid lookupCodeGroupId, LookupCodeParameters lookupCodeParameters, bool trackChanges);
        Task<LookupCodeDto> GetLookupCodeAsync(Guid lookupCodeGroupId, Guid id, bool trackChanges);        
    }
}
