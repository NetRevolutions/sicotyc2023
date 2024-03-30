using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;        

        private readonly Lazy<IAuthenticationManager> _authenticationManager;
        private readonly Lazy<ILookupCodeGroupRepository> _lookupCodeGroupRepository;
        private readonly Lazy<ILookupCodeRepository> _lookupCodeRepository;  
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IUserCompanyRepository> _userCompanyRepository;
        private readonly Lazy<IUserDetailRepository> _userDetailRepository;

        public RepositoryManager(UserManager<User> userManager, IConfiguration configuration, RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _authenticationManager = new Lazy<IAuthenticationManager>(() => new AuthenticationManager(userManager, configuration, repositoryContext));
            _lookupCodeGroupRepository = new Lazy<ILookupCodeGroupRepository>(() => new LookupCodeGroupRepository(repositoryContext));
            _lookupCodeRepository = new Lazy<ILookupCodeRepository>(() => new LookupCodeRepository(repositoryContext));
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _userCompanyRepository = new Lazy<IUserCompanyRepository>(() => new UserCompanyRepository(repositoryContext));
            _userDetailRepository = new Lazy<IUserDetailRepository>(() => new UserDetailRepository(repositoryContext));
        }
        public IAuthenticationManager AuthenticationManager => _authenticationManager.Value;
        public ILookupCodeGroupRepository LookupCodeGroup => _lookupCodeGroupRepository.Value;
        public ILookupCodeRepository LookupCode => _lookupCodeRepository.Value;
        public ICompanyRepository Company => _companyRepository.Value;
        public IUserCompanyRepository UserCompany => _userCompanyRepository.Value;
        public IUserDetailRepository UserDetail => _userDetailRepository.Value;

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
