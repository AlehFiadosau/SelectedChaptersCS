using System.Net.Http;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;

namespace WebCarInspection.Helpers
{
    public class ApiClientHelper : IApiClientHelper
    {
        private readonly HttpClient _client;

        public ApiClientHelper(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUrl, object model) => await _client.PostAsJsonAsync(requestUrl, model);

        public async Task<HttpResponseMessage> GetAsync(string requestUrl) => await _client.GetAsync(requestUrl);

        public async Task<HttpResponseMessage> PutAsync(string requestUrl, object model) => await _client.PutAsJsonAsync(requestUrl, model);

        public async Task<HttpResponseMessage> DeleteAsync(string requestUrl) => await _client.DeleteAsync(requestUrl);
    }
}
