using System.Threading.Tasks;

namespace wine_app.Domain.MapBox
{
    public interface IMapBoxRepository
    {
        Task<Result<MapBoxGeoInformation>> GetGeoInfo(string searchText);
    }
}