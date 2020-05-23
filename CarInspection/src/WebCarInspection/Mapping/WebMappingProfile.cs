using AutoMapper;
using BusinessLayer.Entities;
using WebCarInspection.Models;

namespace WebCarInspection.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<DriverViewModel, Driver>().ReverseMap();
            CreateMap<InspectionViewModel, Inspection>().ReverseMap();
            CreateMap<InspectorViewModel, Inspector>().ReverseMap();
            CreateMap<ViolationViewModel, Violation>().ReverseMap();
            CreateMap<ViolatorViewModel, Violator>().ReverseMap();
        }
    }
}
