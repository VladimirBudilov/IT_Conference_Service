using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Services.Models;

namespace IT_Conference_Service.Services.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Application, ApplicationModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorInfo.Id))
                .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.AuthorInfo.ActivityName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AuthorInfo.Description))
                .ForMember(dest => dest.Outline, opt => opt.MapFrom(src => src.AuthorInfo.Plan))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<ApplicationModel, Application>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AuthorId))
                .ForPath(dest => dest.AuthorInfo.Id, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType))
                .ForPath(dest => dest.AuthorInfo.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
                .ForPath(dest => dest.AuthorInfo.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.AuthorInfo.Plan, opt => opt.MapFrom(src => src.Outline))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));


            CreateMap<Application, ActivityModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ActivityType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AuthorInfo.Description));

            CreateMap<ApplicationModel, AuthorInfo>()
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Plan, opt => opt.MapFrom(src => src.Outline));

                
        }
    }
}
