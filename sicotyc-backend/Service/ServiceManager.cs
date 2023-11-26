using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ILookupCodeGroupService> _lookupCodeGroupService;
        private readonly Lazy<ILookupCodeService> _lookupCodeService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _lookupCodeGroupService = new Lazy<ILookupCodeGroupService>(() => new LookupCodeGroupService(repositoryManager, logger));
            _lookupCodeService = new Lazy<ILookupCodeService>(() => new LookupCodeService(repositoryManager,logger));
        }

        public ILookupCodeGroupService LookupCodeGroupService => _lookupCodeGroupService.Value;

        public ILookupCodeService LookupCodeService => _lookupCodeService.Value;
    }
}
