using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
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
            var result = await _client.GetAsync($"drivers/grtDriver/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await result.Content.ReadAsAsync<DriverViewModel>();
                ViewBag.FirstName = data.FirstName;
            }

            return View();
        }
    }
}
