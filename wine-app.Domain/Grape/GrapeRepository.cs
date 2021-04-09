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

        #region grape
        public async Task<Result<IEnumerable<Grape>>> GetGrapes()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var grapes = JsonConvert.DeserializeObject<IEnumerable<Grape>>(json.Result);

                return new Result<IEnumerable<Grape>>(grapes, true);
            }
            else
            {
                return await HttpResponseHandler.HandleError<IEnumerable<Grape>>(response).ConfigureAwait(false);
            }
        }

        public async Task<Result<Grape>> GetGrape(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_controllerUrl}/{id}");
            var client = _httpClient.CreateClient(ApiNames.WineApi);

            var response = await client.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync();
                var grape = JsonConvert.DeserializeObject<Grape>(json.Result);

                return new Result<Grape>(grape, true);
            }
            else
            {
                return await HttpResponseHandler.HandleError<Grape>(response).ConfigureAwait(false);
            }
        }

        #endregion

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
                return await HttpResponseHandler.HandleError<IEnumerable<GrapeColour>>(response).ConfigureAwait(false);
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
                return await HttpResponseHandler.HandleError<GrapeColour>(response).ConfigureAwait(false);
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
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
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
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
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
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
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
                return await HttpResponseHandler.HandleError(response).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
