using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class LookupCodeService : ILookupCodeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public LookupCodeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public LookupCodeDto GetLookupCode(Guid lookupCodeGroupId, Guid id, bool trackChanges)
        {
            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(lookupCodeGroupId, trackChanges);
            if (lookupCodeGroup == null)
            {
                throw new LookupCodeGroupNotFoundException(lookupCodeGroupId);
            }

            var lookupCodeDb = _repository.LookupCode.GetLookupCode(lookupCodeGroupId, id, trackChanges);
            if (lookupCodeDb == null) 
            {
                throw new LookupCodeNotFoundException(id);
            }

            var lookupCode = _mapper.Map<LookupCodeDto>(lookupCodeDb);
            return lookupCode;
        }

        public IEnumerable<LookupCodeDto> GetLookupCodes(Guid lookupCodeGroupId, bool trackChanges)
        {
            var lookupCodeGroup = _repository.LookupCodeGroup.GetLookupCodeGroup(lookupCodeGroupId, trackChanges);
            if (lookupCodeGroup == null)
            {
                throw new LookupCodeGroupNotFoundException(lookupCodeGroupId);
            }

            var lookupCodesFromDb = _repository.LookupCode.GetLookupCodes(lookupCodeGroupId, trackChanges);
            var lookupCodesDto = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodesFromDb);

            return lookupCodesDto;
        }
    }
}
