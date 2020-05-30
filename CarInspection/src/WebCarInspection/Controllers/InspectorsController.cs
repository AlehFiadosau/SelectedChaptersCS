using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class InspectorsController : Controller
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
        [AllowAnonymous]
        public async Task<IActionResult> ShowInspectors()
        {
            try
            {
                var inspectors = await _inspectorService.GetAllAsync();
                var data = _mapper.Map<List<InspectorViewModel>>(inspectors);

                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<InspectorViewModel>();

                return View(data);
            }
        }

        [HttpGet]
        public IActionResult CreateInspector()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateInspector(InspectorViewModel inspector)
        {
            if (!ModelState.IsValid)
            {
                return CreateInspector(inspector);
            }

            return CreateInspectorInternal(inspector);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInspector(int id)
        {
            var inspector = await _inspectorService.GetByIdAsync(id);
            var mapInspector = _mapper.Map<InspectorViewModel>(inspector);

            return View(mapInspector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInspector(InspectorViewModel inspector)
        {
            try
            {
                var mapInspector = _mapper.Map<Inspector>(inspector);
                await _inspectorService.UpdateAsync(mapInspector);

                return RedirectToAction(nameof(ShowInspectors));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return await UpdateInspector(inspector.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteInspector(int inspectorId)
        {
            var inspector = await _inspectorService.GetByIdAsync(inspectorId);
            var mapInspector = _mapper.Map<Inspector>(inspector);
            await _inspectorService.DeleteAsync(mapInspector);

            return RedirectToAction(nameof(ShowInspectors));
        }

        private async Task<IActionResult> CreateInspectorInternal(InspectorViewModel inspector)
        {
            try
            {
                var mapInspector = _mapper.Map<Inspector>(inspector);
                await _inspectorService.CreateAsync(mapInspector);

                return RedirectToAction(nameof(ShowInspectors));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateInspector();
            }
        }
    }
}