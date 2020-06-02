using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetInspector : ViewComponent
    {
        private readonly IApiClientHelper _client;

        public GetInspector(IApiClientHelper client)
        {
            _client = client;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = await _client.GetAsync($"inspectors/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var data = await _client.ReadAsJsonAsync<InspectorViewModel>(result);
                ViewBag.FirstName = data.FirstName;
            }

            return View();
        }
    }
}
