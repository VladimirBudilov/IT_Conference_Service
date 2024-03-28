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
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.AuthorInfo.PresentationName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.AuthorInfo.DescriptionForWebsie))
                .ForMember(dest => dest.Outline, opt => opt.MapFrom(src => src.AuthorInfo.Plan))
                .ReverseMap();
        }
    }
}
