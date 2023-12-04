using Entities.Models;

namespace Contracts
{
    public interface ILookupCodeRepository
    {
        Task<IEnumerable<LookupCode>> GetLookupCodesAsync(Guid lookupCodeGroupId, bool trackchanges);
        Task<LookupCode> GetLookupCodeAsync(Guid lookupCodeGroupId, Guid id, bool trackChanges);
        Task<int> GetLastLookupCodeOrderAsync(Guid lookupCodeGroupId);
        Task<IEnumerable<LookupCode>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId,  LookupCode lookupCode);        
        void DeleteLookupCode(LookupCode lookupCode);
    }
}
