using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Models;

namespace WebCarInspection.Controllers
{
    public class ViolatorController : Controller
    {
        private readonly IService<Violator, int> _violatorService;
        private readonly IMapper _mapper;

        public ViolatorController(IService<Violator, int> violatorService,
            IMapper mapper)
        {
            _violatorService = violatorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ShowViolators()
        {
            try
            {
                var violators = await _violatorService.GetAllAsync();
                var data = _mapper.Map<List<ViolatorViewModel>>(violators);

                return View(data);
            }
            catch (NotFoundException)
            {
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
                ModelState.AddModelError(string.Empty, ex.Message);

                return CreateViolator();
            }
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
    }
}