using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.RequestFeatures;
using Service.Contracts;

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

        public async Task<LookupCodeDto> GetLookupCodeAsync(Guid lookupCodeGroupId, Guid id, bool trackChanges)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges);
            if (lookupCodeGroup == null)
            {
                throw new LookupCodeGroupNotFoundException(lookupCodeGroupId);
            }

            var lookupCodeDb = await _repository.LookupCode.GetLookupCodeAsync(lookupCodeGroupId, id, trackChanges);
            if (lookupCodeDb == null) 
            {
                throw new LookupCodeNotFoundException(id);
            }

            var lookupCode = _mapper.Map<LookupCodeDto>(lookupCodeDb);
            return lookupCode;
        }

        public async Task<IEnumerable<LookupCodeDto>> GetLookupCodesAsync(Guid lookupCodeGroupId, LookupCodeParameters lookupCodeParameters, bool trackChanges)
        {
            var lookupCodeGroup = await _repository.LookupCodeGroup.GetLookupCodeGroupAsync(lookupCodeGroupId, trackChanges);
            if (lookupCodeGroup == null)
            {
                throw new LookupCodeGroupNotFoundException(lookupCodeGroupId);
            }

            var lookupCodesFromDb = await _repository.LookupCode.GetLookupCodesAsync(lookupCodeGroupId, lookupCodeParameters, trackChanges);
            var lookupCodesDto = _mapper.Map<IEnumerable<LookupCodeDto>>(lookupCodesFromDb);

            return lookupCodesDto;
        }
    }
}
