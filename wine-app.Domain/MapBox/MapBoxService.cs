using System.Threading.Tasks;

namespace wine_app.Domain.MapBox
{
    public class MapBoxService : IMapBoxService
    {
        private readonly IMapBoxRepository _mapBoxRepository;

        public MapBoxService(IMapBoxRepository mapBoxRepository)
        {
            _mapBoxRepository = mapBoxRepository;
        }

        public async Task<Result<MapBoxGeoInformation>> GetGeoInfo(string searchText)
        {
            return await _mapBoxRepository.GetGeoInfo(searchText).ConfigureAwait(false);
        }
    }
}
