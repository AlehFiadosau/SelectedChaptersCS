using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetDriver : ViewComponent
    {
        private readonly IApiClientHelper _client;

        public GetDriver(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = await _client.GetAsync($"drivers/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await _client.ReadAsJsonAsync<DriverViewModel>(result);
                ViewBag.FirstName = data.FirstName;
            }

            return View();
        }
    }
}
