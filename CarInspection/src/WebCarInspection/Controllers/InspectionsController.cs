using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class InspectionsController : Controller
    {
        private readonly IApiClientHelper _client;

        public InspectionsController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowInspections()
        {
            var result = await _client.GetAsync("inspections");
            var data = await result.Content.ReadAsAsync<List<InspectionViewModel>>();

            return View(data);
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
            var result = await _client.GetAsync($"inspections/{id}");
            var data = await result.Content.ReadAsAsync<InspectionViewModel>();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInspection(InspectionViewModel inspection)
        {
            var result = await _client.PutAsync("inspections", inspection);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowInspections));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateInspection(inspection.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteInspection(int id)
        {
            await _client.DeleteAsync($"inspections/{id}");

            return RedirectToAction(nameof(ShowInspections));
        }

        private async Task<IActionResult> CreateInspectionInternal(InspectionViewModel inspection)
        {
            var result = await _client.PostAsync("inspections", inspection);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowInspections));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateInspection();
            }
        }
    }
}