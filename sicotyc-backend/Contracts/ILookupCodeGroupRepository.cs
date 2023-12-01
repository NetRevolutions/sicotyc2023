using Entities.Models;

namespace Contracts
{
    public interface ILookupCodeGroupRepository
    {
        IEnumerable<LookupCodeGroup> GetAllLookupCodeGroups(bool trackChanges);
        LookupCodeGroup GetLookupCodeGroup(Guid lookupCodeGroupId, bool trackChanges);

        IEnumerable<LookupCodeGroup> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void CreateLookupCodeGroup(LookupCodeGroup lookupCodeGroup);
        void DeleteLookupCodeGroup(LookupCodeGroup lookupCodeGroup);
    }
}
