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
    public class ViolatorsController : ControllerBase
    {
        private readonly IService<Violator, int> _violatorService;
        private readonly IMapper _mapper;
        private readonly ILogger<ViolatorsController> _logger;

        public ViolatorsController(IService<Violator, int> violatorService,
            IMapper mapper,
            ILogger<ViolatorsController> logger)
        {
            _violatorService = violatorService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowViolators()
        {
            try
            {
                var violators = await _violatorService.GetAllAsync();
                var data = _mapper.Map<List<ViolatorViewModel>>(violators);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ViolatorViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetViolator(int id)
        {
            var violator = await _violatorService.GetByIdAsync(id);
            var mapViolator = _mapper.Map<ViolatorViewModel>(violator);

            return Ok(mapViolator);
        }

        [HttpPost]
        public async Task<IActionResult> CreateViolator(ViolatorViewModel violator)
        {
            try
            {
                var mapViolator = _mapper.Map<Violator>(violator);
                await _violatorService.CreateAsync(mapViolator);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViolator(ViolatorViewModel violator)
        {
            try
            {
                var mapViolator = _mapper.Map<Violator>(violator);
                await _violatorService.UpdateAsync(mapViolator);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViolator(int id)
        {
            var violator = await _violatorService.GetByIdAsync(id);
            var mapViolator = _mapper.Map<Violator>(violator);
            await _violatorService.DeleteAsync(mapViolator);

            return Ok();
        }
    }
}