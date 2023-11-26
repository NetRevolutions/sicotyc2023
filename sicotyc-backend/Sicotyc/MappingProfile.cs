using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Sicotyc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LookupCodeGroup, LookupCodeGroupDto>();
            CreateMap<LookupCode, LookupCodeDto>();

        }
    }
}
