using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public class GrapeRepository : IGrapeRepository
    {
        private readonly string _controllerUrl = "grape";

        private readonly IHttpClientFactory _httpClient;

        public GrapeRepository(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

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
    }
}
