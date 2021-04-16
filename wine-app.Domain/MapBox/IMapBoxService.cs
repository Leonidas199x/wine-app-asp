using System.Threading.Tasks;

namespace wine_app.Domain.MapBox
{
    public interface IMapBoxService
    {
        Task<Result<MapBoxGeoInformation>> GetGeoInfo(string searchText);
    }
}