using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public class RegionRepository : IRegionRepository
    {
        private readonly string _controllerUrl = "region";

        private readonly IHttpRequestHandler _httpRequestHandler;

        public RegionRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.Region>>>> GetRegions(int page, int pageSize)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}?page={page}&pageSize={pageSize}");
            return await _httpRequestHandler.SendAsync<DataContract.PagedList<IEnumerable<DataContract.Region>>>(request).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Region>> GetRegion(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<DataContract.Region>(request).ConfigureAwait(false);
        }

        public async Task<Result> Create(DataContract.Region region)
        {
            var body = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Update(DataContract.Region region)
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
