﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Sicotyc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LookupCodeGroup, LookupCodeGroupDto>();
            CreateMap<LookupCode, LookupCodeDto>();
            CreateMap<LookupCodeGroupForCreationDto, LookupCodeGroup>()
                .ForMember(d => d.Name, opt => opt.MapFrom(o => o.LookupCodeGroupName));
            CreateMap<LookupCodeForCreationDto, LookupCode>();
            CreateMap<LookupCodeForUpdateDto, LookupCode>()
                .ForMember(d => d.UpdateDtm, opt => opt.MapFrom(o => o.LastUpdatedOn))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LookupCodeGroupForUpdateDto, LookupCodeGroup>()
                .ForMember(d => d.Name, opt => opt.MapFrom(o => o.LookupCodeGroupName))
                .ForMember(d => d.UpdateDtm, opt => opt.MapFrom(o => o.LastUpdatedOn));
            CreateMap<LookupCodeCollectionForCreationDto, LookupCode>();
        }
    }
}
