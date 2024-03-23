using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ILookupCodeRepository
    {
        Task<PagedList<LookupCode>> GetLookupCodesAsync(Guid lookupCodeGroupId, LookupCodeParameters lookupCodeParameters, bool trackChanges);
        Task<IEnumerable<LookupCode>> GetLookupCodesAsync(Guid lookupCodeGroupId, bool trackChanges);
        Task<LookupCode> GetLookupCodeAsync(Guid lookupCodeGroupId, Guid id, bool trackChanges);
        Task<int> GetLastLookupCodeOrderAsync(Guid lookupCodeGroupId);
        Task<IEnumerable<LookupCode>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void CreateLookupCodeForLookupCodeGroup(Guid lookupCodeGroupId,  LookupCode lookupCode);        
        void DeleteLookupCode(LookupCode lookupCode);
    }
}
