using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.RequestFeatures;
using Service.Contracts;

namespace Service
{
    internal sealed class LookupCodeGroupService : ILookupCodeGroupService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public LookupCodeGroupService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository; 
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LookupCodeGroupDto>> GetAllLookupCodeGroupsAsync(LookupCodeGroupParameters lookupCodeGroupParameters, bool trackChanges)
        {
            var lookupCodeGroups = await _repository.LookupCodeGroup.GetAllLookupCodeGroupsAsync(lookupCodeGroupParameters, trackChanges);            

            var lookupCodeGroupsDto = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroups);
            return lookupCodeGroupsDto;
        }

        public async Task<LookupCodeGroupDto> GetLookupCodeGroupAsync(Guid lookupCodeGroupId, bool trackChanges)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges);
            if (lookupCodeGroup == null)
            {
                throw new LookupCodeGroupNotFoundException(lookupCodeGroupId);
            }

            var lookupCodeGroupDto = _mapper.Map<LookupCodeGroupDto>(lookupCodeGroup);
            return lookupCodeGroupDto;
        }
    }
}
