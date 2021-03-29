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

        public async Task<IEnumerable<Country>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Country>>(json.Result);
            }
            else
            {
                return null;
            }
        }

        public async Task<Country> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Country>(json.Result);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Create(Country country)
        {
            var body = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PostAsync(_controllerUrl, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Update(Country country)
        {
            var body = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PutAsync(_controllerUrl, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.DeleteAsync($"{_controllerUrl}/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
