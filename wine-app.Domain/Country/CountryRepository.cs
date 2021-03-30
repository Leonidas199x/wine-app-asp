using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wine_app.Domain.Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string _controllerUrl = "country";

        private readonly IHttpClientFactory _httpClient;

        public CountryRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<IEnumerable<Country>>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(json.Result);

                return new Result<IEnumerable<Country>>(countries, true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result<IEnumerable<Country>>(error, false);
            }
        }

        public async Task<Result<Country>> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var country = JsonConvert.DeserializeObject<Country>(json.Result);

                return new Result<Country>(country, true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result<Country>(error, false);
            }
        }

        public async Task<Result> Create(Country country)
        {
            var body = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PostAsync(_controllerUrl, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false);
            }
        }

        public async Task<Result> Update(Country country)
        {
            var body = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PutAsync(_controllerUrl, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false);
            }
        }

        public async Task<Result> Delete(int Id)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.DeleteAsync($"{_controllerUrl}/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false);
            }
        }
    }
}
