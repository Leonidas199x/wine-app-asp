using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAll();

        Task<Country> Get(int Id);

        Task<Result> Save(Country country, SaveType saveType);

        Task<bool> Delete(int Id);
    }
}
