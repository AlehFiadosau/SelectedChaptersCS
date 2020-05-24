using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetDriver : ViewComponent
    {
        private readonly IService<Driver, int> _driverService;
        private readonly IMapper _mapper;

        public GetDriver(IService<Driver, int> driverService,
            IMapper mapper)
        {
            _driverService = driverService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var driver = await _driverService.GetByIdAsync(id);
                var data = _mapper.Map<DriverViewModel>(driver);

                return View(data);
            }
            catch (NotFoundException)
            {
                var data = new DriverViewModel();

                return View(data);
            }
        }
    }
}
