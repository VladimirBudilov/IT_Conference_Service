using AutoMapper;
using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Services.Models;
using IT_Conference_Service.Validation;

namespace IT_Conference_Service.Services.Mapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Application, ApplicationModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src => src.ActivityType.ToEnumMemberString()))
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.ApplicationInfo.ActivityName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ApplicationInfo.Description))
                .ForMember(dest => dest.Outline, opt => opt.MapFrom(src => src.ApplicationInfo.Outline))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt));

            CreateMap<ApplicationModel, Application>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.ApplicationInfo.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ApplicationInfoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.ActivityType, opt => opt.ConvertUsing(new StringToEnumConverter<ActivityType>(), src => src.ActivityType))
                .ForPath(dest => dest.ApplicationInfo.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
                .ForPath(dest => dest.ApplicationInfo.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.ApplicationInfo.Outline, opt => opt.MapFrom(src => src.Outline))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt));

            CreateMap<Application, ActivityModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ActivityType.ToEnumMemberString()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ApplicationInfo.Description));

            CreateMap<ApplicationModel, ApplicationInfo>()
                .ForMember(dest => dest.ActivityName, opt => opt.MapFrom(src => src.ActivityName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Outline, opt => opt.MapFrom(src => src.Outline));
        }

        public class StringToEnumConverter<T> : IValueConverter<string, T> where T : struct, Enum
        {
            public T Convert(string sourceMember, ResolutionContext context)
            {
                try
                {
                    if (Enum.TryParse(sourceMember, true, out T result))
                    {
                        return result;
                    }
                    throw new ServiceBehaviorException($"Invalid activity type. Cannot convert {sourceMember} to {typeof(T).Name}");
                }
                catch(Exception)
                {
                    throw new ServiceBehaviorException($"Invalid activity type. Cannot convert {sourceMember} to {typeof(T).Name}.");
                }
            }
        }
    }
}
