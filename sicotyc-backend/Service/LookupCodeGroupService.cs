using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    internal sealed class LookupCodeGroupService : ILookupCodeGroupService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public LookupCodeGroupService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository; 
            _logger = logger;
        }

        public IEnumerable<LookupCodeGroup> GetAllLookupCodeGroups(bool trackChanges)
        {
            try
            {
                var lookupCodeGroups = _repository.LookupCodeGroup.GetAllLookupCodeGroups(trackChanges);
                return lookupCodeGroups;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Algo salio mal en el {nameof(GetAllLookupCodeGroups)} service metodo {ex}");
                throw;
            }
        }

    }
}
