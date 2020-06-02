using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetDrivers : ViewComponent
    {
        private readonly IApiClientHelper _client;

        public GetDrivers(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _client.GetAsync("drivers");
            var data = await _client.ReadAsJsonAsync<List<DriverViewModel>>(result);
            ViewBag.Drivers = new SelectList(data, "Id", "FirstName");

            return View();
        }
    }
}
