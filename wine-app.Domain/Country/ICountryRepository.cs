using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryRepository
    {
        Task<Result<IEnumerable<Country>>> GetAll();

        Task<Result<Country>> Get(int id);

        Task<Result> Create(Country country);

        Task<Result> Update(Country country);

        Task<Result> Delete(int id);
    }
}
