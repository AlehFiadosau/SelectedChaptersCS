using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebCarInspection.Interfaces;
using WebCarInspection.ServerInfo;

namespace WebCarInspection.Helpers
{
    public class ApiClientHelper : IApiClientHelper
    {
        private readonly HttpClient _client;
        private readonly IOptions<ApiServerInfo> _apiServerInfo;

        public ApiClientHelper(HttpClient client,
            IOptions<ApiServerInfo> apiServerInfo)
        {
            _client = client;
            _apiServerInfo = apiServerInfo;

            _client.BaseAddress = new Uri(_apiServerInfo.Value.BaseAddress);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUrl, object model)
        {
            var result = await _client.PostAsJsonAsync(requestUrl, model);

            return result;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            var result = await _client.GetAsync(requestUrl);

            return result;
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUrl, object model)
        {
            var result = await _client.PutAsJsonAsync(requestUrl, model);

            return result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUrl)
        {
            var result = await _client.DeleteAsync(requestUrl);

            return result;
        }
    }
}
