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
    public class ViolatorsController : Controller
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

        public async Task<IActionResult> ShowViolators()
        {
            try
            {
                var violators = await _violatorService.GetAllAsync();
                var data = _mapper.Map<List<ViolatorViewModel>>(violators);

                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ViolatorViewModel>();

                return View(data);
            }
        }

        [HttpGet]
        public IActionResult CreateViolator()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateViolator(ViolatorViewModel inspector)
        {
            if (!ModelState.IsValid)
            {
                return CreateViolator(inspector);
            }

            return CreateViolatorInternal(inspector);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateViolator(int violatorId)
        {
            var violator = await _violatorService.GetByIdAsync(violatorId);
            var mapViolator = _mapper.Map<ViolatorViewModel>(violator);

            return View(mapViolator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateViolator(ViolatorViewModel violator)
        {
            try
            {
                var mapViolator = _mapper.Map<Violator>(violator);
                await _violatorService.UpdateAsync(mapViolator);

                return RedirectToAction(nameof(ShowViolators));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return await UpdateViolator(violator.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteViolator(int violatorId)
        {
            var violator = await _violatorService.GetByIdAsync(violatorId);
            var mapViolator = _mapper.Map<Violator>(violator);
            await _violatorService.DeleteAsync(mapViolator);

            return RedirectToAction(nameof(ShowViolators));
        }

        private async Task<IActionResult> CreateViolatorInternal(ViolatorViewModel violator)
        {
            try
            {
                var mapViolator = _mapper.Map<Violator>(violator);
                await _violatorService.CreateAsync(mapViolator);

                return RedirectToAction(nameof(ShowViolators));
            }
            catch (DateException ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateViolator();
            }
        }
    }
}