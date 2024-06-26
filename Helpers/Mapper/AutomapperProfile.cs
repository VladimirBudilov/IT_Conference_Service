﻿using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Helpers.Extensions;
using IT_Conference_Service.Services.Models;

namespace IT_Conference_Service.Helpers.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Application, ApplicationModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.ApplicationInfo.ActivityName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ApplicationInfo.Description))
                .ForMember(dest => dest.Outline, opt => opt.MapFrom(src => src.ApplicationInfo.Outline))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt))
                .AfterMap((src, dest) => dest.ActivityType = src.ApplicationInfo.ActivityType.EnumToString());

            CreateMap<ApplicationModel, Application>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.ApplicationInfo.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ApplicationInfoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForPath(dest => dest.ApplicationInfo.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
                .ForPath(dest => dest.ApplicationInfo.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.ApplicationInfo.Outline, opt => opt.MapFrom(src => src.Outline))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt))
                .AfterMap((src, dest) => dest.ApplicationInfo.ActivityType = src.ActivityType.ToEnum<ActivityType>());

            CreateMap<Application, ActivityModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ApplicationInfo.Description))
                .AfterMap((src, dest) => dest.Type = src.ApplicationInfo.ActivityType.EnumToString());

            CreateMap<ApplicationModel, ApplicationInfo>()
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Outline, opt => opt.MapFrom(src => src.Outline))
                .AfterMap((src, dest) => dest.ActivityType = src.ActivityType.ToEnum<ActivityType>());
        }
    }
}
