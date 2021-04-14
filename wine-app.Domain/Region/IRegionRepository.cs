using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public interface IRegionRepository
    {
        Task<Result<IEnumerable<Region>>> GetRegions();

        Task<Result<Region>> GetRegion(int id);

        Task<Result> Create(Region region);

        Task<Result> Update(Region region);

        Task<Result> Delete(int id);
    }
}