namespace Contracts
{
    public interface IRepositoryManager
    {
        ILookupCodeGroupRepository LookupCodeGroup { get; }
        ILookupCodeRepository LookupCode { get; }
        IAuthenticationManager AuthenticationManager { get; }
        Task SaveAsync();
    }
}
