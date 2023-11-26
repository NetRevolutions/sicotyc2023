using Entities.Models;

namespace Service.Contracts
{
    public interface ILookupCodeGroupService
    {
        IEnumerable<LookupCodeGroup> GetAllLookupCodeGroups(bool trackChanges);
    }
}
