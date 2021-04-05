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

        private readonly IHttpClientFactory _httpClient;

        public GrapeRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        #region grape colour
        public async Task<Result<IEnumerable<GrapeColour>>> GetAllColours()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/colour");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var grapeColours = JsonConvert.DeserializeObject<IEnumerable<GrapeColour>>(json.Result);

                return new Result<IEnumerable<GrapeColour>>(grapeColours, true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result<IEnumerable<GrapeColour>>(error, false);
            }
        }

        public async Task<Result<GrapeColour>> GetColour(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/colour/{id}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var grapeColour = JsonConvert.DeserializeObject<GrapeColour>(json.Result);

                return new Result<GrapeColour>(grapeColour, true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result<GrapeColour>(error, false);
            }
        }

        public async Task<Result> CreateColour(GrapeColour grapeColour)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grapeColour), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PostAsync(_grapeColourUrl, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false);
            }
        }

        public async Task<Result> UpdateColour(GrapeColour grapeColour)
        {
            var body = new StringContent(JsonConvert.SerializeObject(grapeColour), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.PutAsync(_grapeColourUrl, body).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false);
            }
        }

        public async Task<Result> Delete(int Id)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.DeleteAsync($"{_grapeColourUrl}/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false, response.StatusCode);
            }
        }

        public async Task<Result> DeleteColour(int Id)
        {
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.DeleteAsync($"{_grapeColourUrl}/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return new Result(true);
            }
            else
            {
                var error = await HttpResponseHandler.HandleHttpError(response).ConfigureAwait(false);
                return new Result(error, false, response.StatusCode);
            }
        }

        #endregion
    }
}
