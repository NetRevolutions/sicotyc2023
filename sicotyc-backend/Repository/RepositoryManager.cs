using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;        

        private readonly Lazy<ILookupCodeGroupRepository> _lookupCodeGroupRepository;
        private readonly Lazy<ILookupCodeRepository> _lookupCodeRepository;        
        private readonly Lazy<IAuthenticationManager> _authenticationManager;

        public RepositoryManager(UserManager<User> userManager, IConfiguration configuration, RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _lookupCodeGroupRepository = new Lazy<ILookupCodeGroupRepository>(() => new LookupCodeGroupRepository(repositoryContext));
            _lookupCodeRepository = new Lazy<ILookupCodeRepository>(() => new LookupCodeRepository(repositoryContext));
            _authenticationManager = new Lazy<IAuthenticationManager>(() => new AuthenticationManager(userManager, configuration, repositoryContext));
        }
        public ILookupCodeGroupRepository LookupCodeGroup => _lookupCodeGroupRepository.Value;

        public ILookupCodeRepository LookupCode => _lookupCodeRepository.Value;

        public IAuthenticationManager AuthenticationManager => _authenticationManager.Value;

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
