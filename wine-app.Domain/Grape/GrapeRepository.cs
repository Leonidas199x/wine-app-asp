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
        public async Task<Result<IEnumerable<Grape>>> GetGrapes()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}");
            return await _httpRequestHandler.SendAsync<IEnumerable<Grape>>(request).ConfigureAwait(false);
        }

        public async Task<Result<Grape>> GetGrape(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            return await _httpRequestHandler.SendAsync<Grape>(request).ConfigureAwait(false);
        }

        public async Task<Result> CreateGrape(Grape grape)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grape), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_controllerUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> UpdateGrape(Grape grape)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grape), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync($"{_controllerUrl}/{grape.Id}", body).ConfigureAwait(false);
        }

        #endregion

        #region grape colour
        public async Task<Result<IEnumerable<GrapeColour>>> GetAllColours()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/colour");
            return await _httpRequestHandler.SendAsync<IEnumerable<GrapeColour>>(request).ConfigureAwait(false);
        }

        public async Task<Result<GrapeColour>> GetColour(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/colour/{id}");
            return await _httpRequestHandler.SendAsync<GrapeColour>(request).ConfigureAwait(false);
        }

        public async Task<Result> CreateColour(GrapeColour grapeColour)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grapeColour), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PostAsync(_grapeColourUrl, body).ConfigureAwait(false);
        }

        public async Task<Result> UpdateColour(GrapeColour grapeColour)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grapeColour), Encoding.UTF8, "application/json");
            return await _httpRequestHandler.PutAsync($"{_grapeColourUrl}/{grapeColour.Id}", body).ConfigureAwait(false);
        }

        public async Task<Result> DeleteColour(int Id)
        {
            var url = $"{_grapeColourUrl}/{Id}";
            return await _httpRequestHandler.DeleteAsync(url).ConfigureAwait(false);
        }
        #endregion
    }
}
