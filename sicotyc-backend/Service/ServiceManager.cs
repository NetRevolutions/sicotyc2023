using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILookupCodeGroupService> _lookupCodeGroupService;
        private readonly Lazy<ILookupCodeService> _lookupCodeService;
        private readonly Lazy<ISearchService> _searchService;

        public ServiceManager(IRepositoryManager repositoryManager, 
                                ILoggerManager logger, 
                                IMapper mapper, 
                                UserManager<User> userManager, 
                                IAuthenticationManager authManager)
        {
            _lookupCodeGroupService = new Lazy<ILookupCodeGroupService>(() => new LookupCodeGroupService(repositoryManager, logger, mapper));
            _lookupCodeService = new Lazy<ILookupCodeService>(() => new LookupCodeService(repositoryManager,logger, mapper));
            _searchService = new Lazy<ISearchService>(() => new SearchService(repositoryManager, mapper));
        }

        public ILookupCodeGroupService LookupCodeGroupService => _lookupCodeGroupService.Value;

        public ILookupCodeService LookupCodeService => _lookupCodeService.Value;

        public ISearchService SearchService => _searchService.Value;
    }
}
