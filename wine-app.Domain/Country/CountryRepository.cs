using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wine_app.Domain.Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IHttpClientFactory _httpClient;

        public CountryRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "country/countries");
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
            var request = new HttpRequestMessage(HttpMethod.Get, $"country/{id}");
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
    }
}
