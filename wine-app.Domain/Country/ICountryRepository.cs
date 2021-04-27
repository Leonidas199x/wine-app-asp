using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryRepository
    {
        Task<Result<PagedList<IEnumerable<Country>>>> GetAll(int page, int pageSize);

        Task<Result<Country>> Get(int id);

        Task<Result> Create(Country country);

        Task<Result> Update(Country country);

        Task<Result> Delete(int id);
    }
}
