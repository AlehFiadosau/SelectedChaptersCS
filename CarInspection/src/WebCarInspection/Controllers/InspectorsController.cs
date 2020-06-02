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
    public class InspectorsController : Controller
    {
        private readonly IApiClientHelper _client;

        public InspectorsController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowInspectors()
        {
            var result = await _client.GetAsync("inspectors");
            var data = await result.Content.ReadAsAsync<List<InspectorViewModel>>();

            return View(data);
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
            var result = await _client.GetAsync($"inspectors/{id}");
            var data = await result.Content.ReadAsAsync<InspectorViewModel>();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInspector(InspectorViewModel inspector)
        {
            var result = await _client.PutAsync("inspectors", inspector);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowInspectors));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateInspector(inspector.Id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteInspector(int id)
        {
            await _client.DeleteAsync($"inspectors/{id}");

            return RedirectToAction(nameof(ShowInspectors));
        }

        private async Task<IActionResult> CreateInspectorInternal(InspectorViewModel inspector)
        {
            var result = await _client.PostAsync("inspectors", inspector);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowInspectors));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateInspector();
            }
        }
    }
}