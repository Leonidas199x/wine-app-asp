using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryService
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Country>>>> GetAll(int page, int pageSize);

        Task<Result<IEnumerable<DataContract.CountryLookup>>> GetLookup();

        Task<Result<DataContract.Country>> Get(int id);

        Task<Result> Save(DataContract.Country country, SaveType saveType);

        Task<Result> Delete(int id);
    }
}
