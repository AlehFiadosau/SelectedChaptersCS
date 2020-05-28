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
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class InspectionsController : Controller
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
        [AllowAnonymous]
        public async Task<IActionResult> ShowInspections()
        {
            try
            {
                var inspections = await _inspectionService.GetAllAsync();
                var data = _mapper.Map<List<InspectionViewModel>>(inspections);
               
                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<InspectionViewModel>();
                
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult CreateInspection()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateInspection(InspectionViewModel inspection)
        {
            if (!ModelState.IsValid)
            {
                return CreateInspection(inspection);
            }

            return CreateInspectionInternal(inspection);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInspection(int id)
        {
            var inspection = await _inspectionService.GetByIdAsync(id);
            var mapInspection = _mapper.Map<InspectionViewModel>(inspection);

            return View(mapInspection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInspection(InspectionViewModel inspection)
        {
            try
            {
                var mapInspection = _mapper.Map<Inspection>(inspection);
                await _inspectionService.UpdateAsync(mapInspection);

                return RedirectToAction(nameof(ShowInspections));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return await UpdateInspection(inspection.Id);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return await UpdateInspection(inspection.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteInspection(int inspectionId)
        {
            var inspection = await _inspectionService.GetByIdAsync(inspectionId);
            var mapInspection = _mapper.Map<Inspection>(inspection);
            await _inspectionService.DeleteAsync(mapInspection);

            return RedirectToAction(nameof(ShowInspections));
        }

        private async Task<IActionResult> CreateInspectionInternal(InspectionViewModel inspection)
        {
            try
            {
                var mapInspection = _mapper.Map<Inspection>(inspection);
                await _inspectionService.CreateAsync(mapInspection);

                return RedirectToAction(nameof(ShowInspections));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateInspection();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateInspection();
            }
        }
    }
}