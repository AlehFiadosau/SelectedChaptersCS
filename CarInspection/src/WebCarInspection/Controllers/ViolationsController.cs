using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class ViolationsController : Controller
    {
        private readonly IApiClientHelper _client;

        public ViolationsController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowViolations()
        {
            var result = await _client.GetAsync("violations");
            var data = await _client.ReadAsJsonAsync<List<ViolationViewModel>>(result);

            return View(data);
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
        public async Task<IActionResult> UpdateViolation(int id)
        {
            var result = await _client.GetAsync($"violations/{id}");
            var data = await _client.ReadAsJsonAsync<ViolationViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateViolation(ViolationViewModel violation)
        {
            await _client.PutAsync("violations", violation);
            return RedirectToAction(nameof(ShowViolations));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteViolation(int id)
        {
            await _client.DeleteAsync($"violations/{id}");

            return RedirectToAction(nameof(ShowViolations));
        }

        private async Task<IActionResult> CreateViolationInternal(ViolationViewModel violation)
        {
            await _client.PostAsync("violations", violation);
            return RedirectToAction(nameof(ShowViolations));
        }
    }
}