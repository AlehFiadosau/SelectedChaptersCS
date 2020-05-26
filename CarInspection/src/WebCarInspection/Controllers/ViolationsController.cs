using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    public class ViolationsController : Controller
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

        public async Task<IActionResult> ShowViolations()
        {
            try
            {
                var violations = await _violationService.GetAllAsync();
                var data = _mapper.Map<List<ViolationViewModel>>(violations);

                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ViolationViewModel>();

                return View(data);
            }
        }

        [HttpGet]
        public IActionResult CreateViolation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateViolation(ViolationViewModel inspector)
        {
            if (!ModelState.IsValid)
            {
                return CreateViolation(inspector);
            }

            return CreateViolationInternal(inspector);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateViolation(int violationId)
        {
            var violation = await _violationService.GetByIdAsync(violationId);
            var mapViolation = _mapper.Map<ViolationViewModel>(violation);

            return View(mapViolation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateViolation(ViolationViewModel violation)
        {
            var mapViolation = _mapper.Map<Violation>(violation);
            await _violationService.UpdateAsync(mapViolation);

            return RedirectToAction(nameof(ShowViolations));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteViolation(int violationId)
        {
            var violation = await _violationService.GetByIdAsync(violationId);
            var mapViolation = _mapper.Map<Violation>(violation);
            await _violationService.DeleteAsync(mapViolation);

            return RedirectToAction(nameof(ShowViolations));
        }

        private async Task<IActionResult> CreateViolationInternal(ViolationViewModel violation)
        {
            var mapViolation = _mapper.Map<Violation>(violation);
            await _violationService.CreateAsync(mapViolation);

            return RedirectToAction(nameof(ShowViolations));
        }
    }
}