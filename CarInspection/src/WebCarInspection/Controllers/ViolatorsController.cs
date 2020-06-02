using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class ViolatorsController : Controller
    {
        private readonly IApiClientHelper _client;

        public ViolatorsController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowViolators()
        {
            var result = await _client.GetAsync("violators");
            var data = await _client.ReadAsJsonAsync<List<ViolatorViewModel>>(result);

            return View(data);
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
        public async Task<IActionResult> UpdateViolator(int id)
        {
            var result = await _client.GetAsync($"violators/{id}");
            var data = await _client.ReadAsJsonAsync<ViolatorViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateViolator(ViolatorViewModel violator)
        {
            var result = await _client.PutAsync("violators", violator);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowViolators));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateViolator(violator.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteViolator(int id)
        {
            await _client.DeleteAsync($"violators/{id}");

            return RedirectToAction(nameof(ShowViolators));
        }

        private async Task<IActionResult> CreateViolatorInternal(ViolatorViewModel violator)
        {
            var result = await _client.PostAsync("violators", violator);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowViolators));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateViolator();
            }
        }
    }
}