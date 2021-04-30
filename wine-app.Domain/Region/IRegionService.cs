using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public interface IRegionService
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Region>>>> GetRegions(int page, int pageSize);

        Task<Result<DataContract.Region>> GetRegion(int id);

        Task<Result> Save(DataContract.Region region, SaveType saveType);

        Task<Result> Delete(int id);
    }
}