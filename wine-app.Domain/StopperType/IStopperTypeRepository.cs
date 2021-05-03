using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.StopperType
{
    public interface IStopperTypeRepository
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.StopperType>>>> GetStopperTypes(int page, int pageSize);

        Task<Result<DataContract.StopperType>> Get(int id);

        Task<Result> Create(DataContract.StopperType drinker);

        Task<Result> Update(DataContract.StopperType drinker);

        Task<Result> Delete(int id);
    }
}