using AutoMapper;
using HealthDeclarationAPI.Entities;
using HealthDeclarationAPI.Models;

namespace HealthDeclarationAPI.Utilities {
    public class AutoMapperProfile: Profile {
        public AutoMapperProfile()
        {
            //CreateMap<HealthDeclarationEntity, HealthDeclarationModel>();
                //.ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.Data));
                //.ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.DeviceType))
                //.ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName))
                //.ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
                //.ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.DataType))
                //.ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        }
    }
}
