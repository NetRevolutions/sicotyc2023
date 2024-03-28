using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ILookupCodeGroupRepository
    {
        Task<PagedList<LookupCodeGroup>> GetAllLookupCodeGroupsAsync(LookupCodeGroupParameters lookupCodeGroupParameters,  bool trackChanges);
        Task<IEnumerable<LookupCodeGroup>> GetAllLookupCodeGroupsAsync(bool trackChanges);
        Task<bool> ExistsLookupCodeGroupAsync(string lookupCodeGroupName, bool trackChanges);
        Task<LookupCodeGroup> GetLookupCodeGroupAsync(Guid lookupCodeGroupId, bool trackChanges);
        Task<LookupCodeGroup> GetLookupCodeGroupByNameAsync(string name, bool trackChanges);

        Task<IEnumerable<LookupCodeGroup>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void CreateLookupCodeGroup(LookupCodeGroup lookupCodeGroup);
        void DeleteLookupCodeGroup(LookupCodeGroup lookupCodeGroup);
    }
}
