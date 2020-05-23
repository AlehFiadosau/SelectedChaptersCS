using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Models;

namespace WebCarInspection.Controllers
{
    public class DriverController : Controller
    {
        private readonly IService<Driver, int> _driverService;
        private readonly IMapper _mapper;

        public DriverController(IService<Driver, int> driverService,
            IMapper mapper)
        {
            _driverService = driverService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ShowDrivers()
        {
            try
            {
                var drivers = await _driverService.GetAllAsync();
                var data = _mapper.Map<List<DriverViewModel>>(drivers);

                return View(data);
            }
            catch (NotFoundException)
            {
                var data = new List<DriverViewModel>();

                return View(data);
            }
        }

        [HttpGet]
        public IActionResult CreateDriver()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateDriver(DriverViewModel driver)
        {
            if (!ModelState.IsValid)
            {
                return CreateDriver(driver);
            }

            return CreateDriverInternal(driver);
        }

        private async Task<IActionResult> CreateDriverInternal(DriverViewModel driver)
        {
            try
            {
                var mapDriver = _mapper.Map<Driver>(driver);
                await _driverService.CreateAsync(mapDriver);

                return RedirectToAction(nameof(ShowDrivers));
            }
            catch (DateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateDriver();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDriver(int driverId)
        {
            var driver = await _driverService.GetByIdAsync(driverId);
            var mapDriver = _mapper.Map<DriverViewModel>(driver);

            return View(mapDriver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDriver(DriverViewModel driver)
        {
            try
            {
                var mapDriver = _mapper.Map<Driver>(driver);
                await _driverService.UpdateAsync(mapDriver);

                return RedirectToAction(nameof(ShowDrivers));
            }
            catch (DateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return await UpdateDriver(driver.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDriver(int driverId)
        {
            var driver = await _driverService.GetByIdAsync(driverId);
            var mapDriver = _mapper.Map<Driver>(driver);
            await _driverService.DeleteAsync(mapDriver);

            return RedirectToAction(nameof(ShowDrivers));
        }
    }
}