using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetDrivers : ViewComponent
    {
        private readonly IService<Driver, int> _driverService;
        private readonly IMapper _mapper;

        public GetDrivers(IService<Driver, int> driverService,
            IMapper mapper)
        {
            _driverService = driverService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var drivers = await _driverService.GetAllAsync();
                var data = _mapper.Map<List<DriverViewModel>>(drivers);
                ViewBag.Drivers = new SelectList(data, "Id", "FirstName");
            }
            catch (NotFoundException)
            {
                var data = new List<DriverViewModel>();
                ViewBag.Drivers = new SelectList(data, "Id", "FirstName");
            }

            return View();
        }
    }
}
