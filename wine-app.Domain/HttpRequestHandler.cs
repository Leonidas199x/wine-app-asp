using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace wine_app.Domain
{
    public class HttpRequestHandler : IHttpRequestHandler
    {
        private const string error = "Cannot connect to the wine API. If the issue persists, please contact support.";
        private readonly IHttpClientFactory _httpClient;

        public HttpRequestHandler(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<T>> SendAsync<T>(HttpRequestMessage request)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);
            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(json.Result);

                return new Result<T>(data, response.IsSuccessStatusCode);
            }
            else
            {
                return await HttpResponseHandler.HandleError<T>(response).ConfigureAwait(false);
            }
        }

        public async Task<Result> PostAsync(string url, StringContent body)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);
            var response = await client.PostAsync(url, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(response.IsSuccessStatusCode);
            }
            else
            {
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
            }
        }

        public async Task<Result> PutAsync(string url, StringContent body)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PutAsync(url, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(response.IsSuccessStatusCode);
            }
            else
            {
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
            }
        }

        public async Task<Result> DeleteAsync(string url)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.DeleteAsync(url).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(response.IsSuccessStatusCode);
            }
            else
            {
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
            }
        }
    }
}
