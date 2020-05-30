using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = RoleNames.User)]
    public class DriversController : Controller
    {
        private readonly IService<Driver, int> _driverService;
        private readonly IMapper _mapper;
        private readonly ILogger<DriversController> _logger;

        public DriversController(IService<Driver, int> driverService,
            IMapper mapper,
            ILogger<DriversController> logger)
        {
            _driverService = driverService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowDrivers()
        {
            try
            {
                var drivers = await _driverService.GetAllAsync();
                var data = _mapper.Map<List<DriverViewModel>>(drivers);

                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
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

        [HttpGet]
        public async Task<IActionResult> UpdateDriver(int id)
        {
            var driver = await _driverService.GetByIdAsync(id);
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
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
                ModelState.AddModelError("DataException", ex.Message);

                return CreateDriver();
            }
        }
    }
}