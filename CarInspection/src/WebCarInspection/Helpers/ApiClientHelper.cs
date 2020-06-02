using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
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

        public async Task<HttpResponseMessage> PostAsync(string requestUrl, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await _client.PostAsync(requestUrl, stringContent);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUrl) => await _client.GetAsync(requestUrl);

        public async Task<HttpResponseMessage> PutAsync(string requestUrl, object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await _client.PutAsync(requestUrl, stringContent);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUrl) => await _client.DeleteAsync(requestUrl);

        public async Task<T> ReadAsJsonAsync<T>(HttpResponseMessage httpMessage)
        {
            var content = await httpMessage.Content.ReadAsStreamAsync();

            using var streamReader = new StreamReader(content);
            using var jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();

            return serializer.Deserialize<T>(jsonReader);
        }
    }
}
