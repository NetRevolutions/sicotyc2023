namespace Contracts
{
    public interface IRepositoryManager
    {
        IAuthenticationManager AuthenticationManager { get; }
        ILookupCodeGroupRepository LookupCodeGroup { get; }
        ILookupCodeRepository LookupCode { get; }
        ICompanyRepository Company { get; }
        IUserCompanyRepository UserCompany { get; }

        Task SaveAsync();
    }
}
