using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.StopperType
{
    public interface IStopperTypeService
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.StopperType>>>> GetStopperTypes(int page, int pageSize);

        Task<Result<DataContract.StopperType>> Get(int id);

        Task<Result> Save(DataContract.StopperType stopperType, SaveType saveType);

        Task<Result> Delete(int id);
    }
}