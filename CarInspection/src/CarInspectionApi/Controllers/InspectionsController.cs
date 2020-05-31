using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using CarInspectionApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarInspectionApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InspectionsController : ControllerBase
    {
        private readonly IService<Inspection, int> _inspectionService;
        private readonly IMapper _mapper;
        private readonly ILogger<InspectionsController> _logger;

        public InspectionsController(IService<Inspection, int> inspectionService,
            IMapper mapper,
            ILogger<InspectionsController> logger)
        {
            _inspectionService = inspectionService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> ShowInspections()
        {
            try
            {
                var inspections = await _inspectionService.GetAllAsync();
                var data = _mapper.Map<List<InspectionViewModel>>(inspections);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<InspectionViewModel>();

                return Ok(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInspection(InspectionViewModel inspection)
        {
            try
            {
                var mapInspection = _mapper.Map<Inspection>(inspection);
                await _inspectionService.CreateAsync(mapInspection);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }


        [HttpGet("updateInspection/{id}")]
        public async Task<IActionResult> UpdateInspection(int id)
        {
            var inspection = await _inspectionService.GetByIdAsync(id);
            var mapInspection = _mapper.Map<InspectionViewModel>(inspection);

            return Ok(mapInspection);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInspection(InspectionViewModel inspection)
        {
            try
            {
                var mapInspection = _mapper.Map<Inspection>(inspection);
                await _inspectionService.UpdateAsync(mapInspection);

                return Ok();
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspection(int id)
        {
            var inspection = await _inspectionService.GetByIdAsync(id);
            var mapInspection = _mapper.Map<Inspection>(inspection);
            await _inspectionService.DeleteAsync(mapInspection);

            return Ok();
        }
    }
}