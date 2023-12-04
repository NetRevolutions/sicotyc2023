namespace Contracts
{
    public interface IRepositoryManager
    {
        ILookupCodeGroupRepository LookupCodeGroup { get; }
        ILookupCodeRepository LookupCode { get; }
        Task SaveAsync();
    }
}
