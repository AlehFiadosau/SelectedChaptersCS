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
    public class InspectorsController : ControllerBase
    {
        private readonly IService<Inspector, int> _inspectorService;
        private readonly IMapper _mapper;
        private readonly ILogger<InspectorsController> _logger;

        public InspectorsController(IService<Inspector, int> inspectorService,
            IMapper mapper,
            ILogger<InspectorsController> logger)
        {
            _inspectorService = inspectorService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ShowInspectors()
        {
            try
            {
                var inspectors = await _inspectorService.GetAllAsync();
                var data = _mapper.Map<List<InspectorViewModel>>(inspectors);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<InspectorViewModel>();

                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInspector(int id)
        {
            try
            {
                var driver = await _inspectorService.GetByIdAsync(id);
                var data = _mapper.Map<InspectorViewModel>(driver);

                return Ok(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInspector(InspectorViewModel inspector)
        {
            try
            {
                var mapInspector = _mapper.Map<Inspector>(inspector);
                await _inspectorService.CreateAsync(mapInspector);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInspector(InspectorViewModel inspector)
        {
            try
            {
                var mapInspector = _mapper.Map<Inspector>(inspector);
                await _inspectorService.UpdateAsync(mapInspector);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspector(int id)
        {
            var inspector = await _inspectorService.GetByIdAsync(id);
            var mapInspector = _mapper.Map<Inspector>(inspector);
            await _inspectorService.DeleteAsync(mapInspector);

            return Ok();
        }
    }
}