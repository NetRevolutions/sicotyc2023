using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ILookupCodeGroupRepository> _lookupCodeGroupRepository;
        private readonly Lazy<ILookupCodeRepository> _lookupCodeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _lookupCodeGroupRepository = new Lazy<ILookupCodeGroupRepository>(() => new LookupCodeGroupRepository(repositoryContext));
            _lookupCodeRepository = new Lazy<ILookupCodeRepository>(() => new LookupCodeRepository(repositoryContext));
        }
        public ILookupCodeGroupRepository LookupCodeGroup => _lookupCodeGroupRepository.Value;

        public ILookupCodeRepository LookupCode => _lookupCodeRepository.Value;

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
