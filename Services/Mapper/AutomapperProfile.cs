using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Services.Models;

namespace IT_Conference_Service.Services.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<ApplicationModel, Application>()
                 .ForMember(dest => dest.ActivityType,
                           opt => opt.MapFrom(src => Enum.Parse<ActivityTypeEnum>(src.ActivityType)))
                 .ForMember(dest => dest.AuthorInfo,
                                           opt => opt.MapFrom(src => new AuthorInfo
                                           {
                                               Id = src.AuthorId,
                                               DescriptionForWebsie = src.Description,
                                               Plan = src.Outline
                                           }))
                 .ForMember(dest => dest.AuthorId,
                                           opt => opt.MapFrom(src => src.AuthorId))
                 .ReverseMap();
        }
    }
}
