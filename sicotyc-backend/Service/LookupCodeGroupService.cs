using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

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

        public IEnumerable<LookupCodeGroupDto> GetAllLookupCodeGroups(bool trackChanges)
        {
            var lookupCodeGroups = _repository.LookupCodeGroup.GetAllLookupCodeGroups(trackChanges);

            //var lookupCodeGroupsDto = lookupCodeGroups.Select(l => new LookupCodeGroupDto(l.Id, l.Name ?? "")).ToList();

            var lookupCodeGroupsDto = _mapper.Map<IEnumerable<LookupCodeGroupDto>>(lookupCodeGroups);
            return lookupCodeGroupsDto;
        }

        public LookupCodeGroupDto GetLookupCodeGroup(Guid lookupCodeGroupId, bool trackChanges)
        {
            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(lookupCodeGroupId, trackChanges);
            if (lookupCodeGroup == null)
            {
                throw new LookupCodeGroupNotFoundException(lookupCodeGroupId);
            }

            var lookupCodeGroupDto = _mapper.Map<LookupCodeGroupDto>(lookupCodeGroup);
            return lookupCodeGroupDto;
        }
    }
}
