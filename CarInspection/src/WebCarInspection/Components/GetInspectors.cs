using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetInspectors : ViewComponent
    {
        private readonly IApiClientHelper _client;

        public GetInspectors(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _client.GetAsync("inspectors");
            var data = await _client.ReadAsJsonAsync<List < InspectorViewModel>>(result);
            ViewBag.Inspectors = new SelectList(data, "Id", "FirstName");

            return View();
        }
    }
}
