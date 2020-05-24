using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    public class InspectorsController : Controller
    {
        private readonly IService<Inspector, int> _inspectorService;
        private readonly IMapper _mapper;

        public InspectorsController(IService<Inspector, int> inspectorService,
            IMapper mapper)
        {
            _inspectorService = inspectorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ShowInspectors()
        {
            try
            {
                var inspectors = await _inspectorService.GetAllAsync();
                var data = _mapper.Map<List<InspectorViewModel>>(inspectors);

                return View(data);
            }
            catch (NotFoundException)
            {
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
        public async Task<IActionResult> UpdateInspector(int inspectorId)
        {
            var inspector = await _inspectorService.GetByIdAsync(inspectorId);
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
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateInspector();
            }
        }
    }
}