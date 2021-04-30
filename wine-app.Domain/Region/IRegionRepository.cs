using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public interface IRegionRepository
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Region>>>> GetRegions(int page, int pageSize);

        Task<Result<DataContract.Region>> GetRegion(int id);

        Task<Result> Create(DataContract.Region region);

        Task<Result> Update(DataContract.Region region);

        Task<Result> Delete(int id);
    }
}