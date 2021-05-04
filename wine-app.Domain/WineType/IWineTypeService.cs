using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.WineType
{
    public interface IWineTypeService
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.WineType>>>> GetWineTypes(int page, int pageSize);

        Task<Result<DataContract.WineType>> Get(int id);

        Task<Result> Save(DataContract.WineType wineType, SaveType saveType);

        Task<Result> Delete(int id);
    }
}