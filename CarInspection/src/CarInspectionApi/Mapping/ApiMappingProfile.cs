using AutoMapper;
using BusinessLayer.Entities;
using CarInspectionApi.ViewModels;

namespace CarInspectionApi.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<DriverViewModel, Driver>().ReverseMap();
            CreateMap<InspectionViewModel, Inspection>().ReverseMap();
            CreateMap<InspectorViewModel, Inspector>().ReverseMap();
            CreateMap<ViolationViewModel, Violation>().ReverseMap();
            CreateMap<ViolatorViewModel, Violator>().ReverseMap();
        }
    }
}
