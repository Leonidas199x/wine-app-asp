using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wine_app.Domain.StopperType
{
    public class StopperTypeRepository : IStopperTypeRepository
    {
        private readonly string _controllerUrl = "stoppertype";

        private readonly IHttpRequestHandler _httpRequestHandler;

        public StopperTypeRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.StopperType>>>> GetStopperTypes(int page, int pageSize)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}?page={page}&pageSize={pageSize}");
            return await _httpRequestHandler.SendAsync<DataContract.PagedList<IEnumerable<DataContract.StopperType>>>(request).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.StopperType>> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<DataContract.StopperType>(request).ConfigureAwait(false);
        }

        public async Task<Result> Create(DataContract.StopperType stopperType)
        {
            var body = new StringContent(JsonConvert.SerializeObject(stopperType), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Update(DataContract.StopperType stopperType)
        {
            var body = new StringContent(JsonConvert.SerializeObject(stopperType), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            var url = $"{_controllerUrl}/{id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
    }
}
