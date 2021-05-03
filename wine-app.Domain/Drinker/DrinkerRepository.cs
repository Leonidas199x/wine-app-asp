using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wine_app.Domain.Drinker
{
    public class DrinkerRepository : IDrinkerRepository
    {
        private readonly string _controllerUrl = "drinker";

        private readonly IHttpRequestHandler _httpRequestHandler;

        public DrinkerRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.Drinker>>>> GetDrinkers(int page, int pageSize)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}?page={page}&pageSize={pageSize}");
            return await _httpRequestHandler.SendAsync<DataContract.PagedList<IEnumerable<DataContract.Drinker>>>(request).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Drinker>> GetDrinker(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<DataContract.Drinker>(request).ConfigureAwait(false);
        }

        public async Task<Result> Create(DataContract.Drinker drinker)
        {
            var body = new StringContent(JsonConvert.SerializeObject(drinker), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Update(DataContract.Drinker drinker)
        {
            var body = new StringContent(JsonConvert.SerializeObject(drinker), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            var url = $"{_controllerUrl}/{id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
    }
}
