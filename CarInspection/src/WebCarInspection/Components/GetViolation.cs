using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetViolation : ViewComponent
    {
        private readonly IApiClientHelper _client;

        public GetViolation(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = await _client.GetAsync($"violations/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await result.Content.ReadAsAsync<ViolationViewModel>();
                ViewBag.Name = data.Name;
            }

            return View();
        }
    }
}
