using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ILookupCodeService
    {
        IEnumerable<LookupCodeDto> GetLookupCodes(Guid lookupCodeGroupId, bool trackChanges);
        LookupCodeDto GetLookupCode(Guid lookupCodeGroupId, Guid id, bool trackChanges);
    }
}
