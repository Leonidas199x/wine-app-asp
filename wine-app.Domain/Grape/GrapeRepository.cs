using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public class GrapeRepository : IGrapeRepository
    {
        private readonly string _controllerUrl = "grape";
        private readonly string _grapeColourUrl = "grape/colour";

        private readonly IHttpRequestHandler _httpRequestHandler;

        public GrapeRepository(IHttpRequestHandler httpRequestHandler)
        {
            _httpRequestHandler = httpRequestHandler;
        }

        #region grape
        public async Task<Result<IEnumerable<DataContract.Grape>>> GetGrapes(int page, int pageSize)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}?page={page}&pageSize={pageSize}");
            return await _httpRequestHandler.SendAsync<IEnumerable<DataContract.Grape>>(request).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Grape>> GetGrape(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<DataContract.Grape>(request).ConfigureAwait(false);
        }

        public async Task<Result> CreateGrape(DataContract.Grape grape)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grape), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> UpdateGrape(DataContract.Grape grape)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grape), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync($"{_controllerUrl}/{grape.Id}", body).ConfigureAwait(false);
        }

        public async Task<Result> DeleteGrape(int id)
        {
            var url = $"{_grapeColourUrl}/{id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
        #endregion

        #region grape colour
        public async Task<Result<IEnumerable<DataContract.GrapeColour>>> GetAllColours(int page, int pageSize)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/colour?page={page}&pageSize={pageSize}");
            return await _httpRequestHandler.SendAsync<IEnumerable<DataContract.GrapeColour>>(request).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.GrapeColour>> GetColour(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/colour/{id}");
            return await _httpRequestHandler.SendAsync<DataContract.GrapeColour>(request).ConfigureAwait(false);
        }

        public async Task<Result> CreateColour(DataContract.GrapeColour grapeColour)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grapeColour), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_grapeColourUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> UpdateColour(DataContract.GrapeColour grapeColour)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grapeColour), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync($"{_grapeColourUrl}/{grapeColour.Id}", body).ConfigureAwait(false);
        }

        public async Task<Result> DeleteColour(int id)
        {
            var url = $"{_grapeColourUrl}/{id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
        #endregion
    }
}
