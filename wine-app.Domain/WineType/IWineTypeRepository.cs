using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.WineType
{
    public interface IWineTypeRepository
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.WineType>>>> GetWineTypes(int page, int pageSize);

        Task<Result<DataContract.WineType>> Get(int id);

        Task<Result> Create(DataContract.WineType wineType);

        Task<Result> Update(DataContract.WineType wineType);

        Task<Result> Delete(int id);
    }
}