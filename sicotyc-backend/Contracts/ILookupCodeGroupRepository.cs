using Entities.Models;

namespace Contracts
{
    public interface ILookupCodeGroupRepository
    {
        Task<IEnumerable<LookupCodeGroup>> GetAllLookupCodeGroupsAsync(bool trackChanges);
        Task<LookupCodeGroup> GetLookupCodeGroupAsync(Guid lookupCodeGroupId, bool trackChanges);
        Task<LookupCodeGroup> GetLookupCodeGroupByNameAsync(string name, bool trackChanges);

        Task<IEnumerable<LookupCodeGroup>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void CreateLookupCodeGroup(LookupCodeGroup lookupCodeGroup);
        void DeleteLookupCodeGroup(LookupCodeGroup lookupCodeGroup);
    }
}
