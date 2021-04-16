using System.Net.Http;
using System.Threading.Tasks;

namespace wine_app.Domain.MapBox
{
    public class MapBoxRepository : IMapBoxRepository
    {
        private const string SearchUrl = "{searchText}.json?access_token={accessToken}";
        private readonly IHttpRequestHandler _httpRequestHandler;
        private readonly string _mapboxAccessToken;

        public MapBoxRepository(IHttpRequestHandler httpRequestHandler, string mapboxAccessToken)
        {
            _httpRequestHandler = httpRequestHandler;
            _mapboxAccessToken = mapboxAccessToken;
        }

        public async Task<Result<MapBoxGeoInformation>> GetGeoInfo(string searchText)
        {
            var url = SearchUrl
                .Replace("{searchText}", searchText)
                .Replace("{accessToken}", _mapboxAccessToken);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return await _httpRequestHandler.SendAsync<MapBoxGeoInformation>(request, ApiNames.MapBoxApi).ConfigureAwait(false);
        }
    }
}
