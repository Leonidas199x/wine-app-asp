using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wine_app.Domain.WineType
{
    public class WineTypeRepository : IWineTypeRepository
    {
        private readonly string _controllerUrl = "winetype";

        private readonly IHttpRequestHandler _httpRequestHandler;

        public WineTypeRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.WineType>>>> GetWineTypes(int page, int pageSize)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}?page={page}&pageSize={pageSize}");
            return await _httpRequestHandler.SendAsync<DataContract.PagedList<IEnumerable<DataContract.WineType>>>(request).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.WineType>> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<DataContract.WineType>(request).ConfigureAwait(false);
        }

        public async Task<Result> Create(DataContract.WineType wineType)
        {
            var body = new StringContent(JsonConvert.SerializeObject(wineType), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Update(DataContract.WineType wineType)
        {
            var body = new StringContent(JsonConvert.SerializeObject(wineType), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            var url = $"{_controllerUrl}/{id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
    }
}
