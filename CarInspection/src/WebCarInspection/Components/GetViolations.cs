using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetViolations : ViewComponent
    {
        private readonly IApiClientHelper _client;

        public GetViolations(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _client.GetAsync("violations");
            var data = await _client.ReadAsJsonAsync<List<ViolationViewModel>>(result);
            ViewBag.Violations = new SelectList(data, "Id", "Name");

            return View();
        }
    }
}
