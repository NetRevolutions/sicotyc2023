using Contracts;
using Service.Contracts;

namespace Service
{
    internal sealed class LookupCodeService : ILookupCodeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public LookupCodeService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
