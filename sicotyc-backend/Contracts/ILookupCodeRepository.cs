using Entities.Models;

namespace Contracts
{
    public interface ILookupCodeRepository
    {
        IEnumerable<LookupCode> GetLookupCodes(Guid lookupCodeGroupId, bool trackchanges);
        LookupCode GetLookupCode(Guid lookupCodeGroupId, Guid id, bool trackChanges);
        int GetLastLookupCodeOrder(Guid lookupCodeGroupId);
        void CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId,  LookupCode lookupCode);        
        void DeleteLookupCode(LookupCode lookupCode);
        IEnumerable<LookupCode> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    }
}
