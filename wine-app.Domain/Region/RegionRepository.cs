using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public class RegionRepository :IRegionRepository
    {
        private readonly string _controllerUrl = "region";

        private readonly IHttpRequestHandler _httpRequestHandler;

        public RegionRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public async Task<Result<IEnumerable<Region>>> GetRegions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}");
            return await _httpRequestHandler.SendAsync<IEnumerable<Region>>(request).ConfigureAwait(false);
        }

        public async Task<Result<Region>> GetRegion(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<Region>(request).ConfigureAwait(false);
        }

        public async Task<Result> Create(Region region)
        {
            var body = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Update(Region region)
        {
            var body = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            var url = $"{_controllerUrl}/{id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
    }
}
