using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryService
    {
        Task<Result<PagedList<IEnumerable<Country>>>> GetAll(int page, int pageSize);

        Task<Result<Country>> Get(int id);

        Task<Result> Save(Country country, SaveType saveType);

        Task<Result> Delete(int id);
    }
}
