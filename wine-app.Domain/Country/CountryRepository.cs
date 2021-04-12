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

        private readonly IHttpRequestHandler _httpRequestHandler;

        public CountryRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public async Task<Result<IEnumerable<Country>>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}");
            return await _httpRequestHandler.SendAsync<IEnumerable<Country>>(request).ConfigureAwait(false);
        }

        public async Task<Result<Country>> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<Country>(request).ConfigureAwait(false);
        }

        public async Task<Result> Create(Country country)
        {
            var body = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Update(Country country)
        {
            var body = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int Id)
        {
            var url = $"{_controllerUrl}/{Id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
    }
}
