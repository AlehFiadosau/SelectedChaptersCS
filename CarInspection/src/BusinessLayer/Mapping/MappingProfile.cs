using AutoMapper;
using BusinessLayer.Entities;
using DataAccessLayer.DTO;

namespace BusinessLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Driver, DriverDto>().ReverseMap();
            CreateMap<Inspection, InspectionDto>().ReverseMap();
            CreateMap<Inspector, InspectorDto>().ReverseMap();
            CreateMap<Violation, ViolationDto>().ReverseMap();
            CreateMap<Violator, ViolatorDto>().ReverseMap();
        }
    }
}
