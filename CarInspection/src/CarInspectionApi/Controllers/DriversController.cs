using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspectionApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarInspectionApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
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
        public async Task<IActionResult> ShowDrivers()
        {
            try
            {
                var drivers = await _driverService.GetAllAsync();
                var data = _mapper.Map<List<DriverViewModel>>(drivers);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<DriverViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("getDriver/{id}")]
        public async Task<IActionResult> GetDriver(int id)
        {
            try
            {
                var driver = await _driverService.GetByIdAsync(id);
                var data = _mapper.Map<DriverViewModel>(driver);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver(DriverViewModel driver)
        {
            try
            {
                var mapDriver = _mapper.Map<Driver>(driver);
                await _driverService.CreateAsync(mapDriver);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("updateDriver/{id}")]
        public async Task<IActionResult> UpdateDriver(int id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            var mapDriver = _mapper.Map<DriverViewModel>(driver);

            return Ok(mapDriver);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriver(DriverViewModel driver)
        {
            try
            {
                var mapDriver = _mapper.Map<Driver>(driver);
                await _driverService.UpdateAsync(mapDriver);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _driverService.GetByIdAsync(id);
            var mapDriver = _mapper.Map<Driver>(driver);
            await _driverService.DeleteAsync(mapDriver);

            return Ok();
        }
    }
}