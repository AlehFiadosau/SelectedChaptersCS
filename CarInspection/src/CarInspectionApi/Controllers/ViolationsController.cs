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
    public class ViolationsController : ControllerBase
    {
        private readonly IService<Violation, int> _violationService;
        private readonly IMapper _mapper;
        private readonly ILogger<ViolationsController> _logger;

        public ViolationsController(IService<Violation, int> violationService,
            IMapper mapper,
            ILogger<ViolationsController> logger)
        {
            _violationService = violationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowViolations()
        {
            try
            {
                var violations = await _violationService.GetAllAsync();
                var data = _mapper.Map<List<ViolationViewModel>>(violations);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ViolationViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("getViolation/{id}")]
        public async Task<IActionResult> GetViolation(int id)
        {
            try
            {
                var driver = await _violationService.GetByIdAsync(id);
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
        public async Task<IActionResult> CreateViolation(ViolationViewModel violation)
        {
            var mapViolation = _mapper.Map<Violation>(violation);
            await _violationService.CreateAsync(mapViolation);

            return Ok();
        }

        [HttpGet("updateViolation/{id}")]
        public async Task<IActionResult> UpdateViolation(int id)
        {
            var violation = await _violationService.GetByIdAsync(id);
            var mapViolation = _mapper.Map<ViolationViewModel>(violation);

            return Ok(mapViolation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViolation(ViolationViewModel violation)
        {
            var mapViolation = _mapper.Map<Violation>(violation);
            await _violationService.UpdateAsync(mapViolation);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViolation(int id)
        {
            var violation = await _violationService.GetByIdAsync(id);
            var mapViolation = _mapper.Map<Violation>(violation);
            await _violationService.DeleteAsync(mapViolation);

            return Ok();
        }
    }
}