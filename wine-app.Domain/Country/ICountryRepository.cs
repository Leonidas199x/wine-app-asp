using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAll();

        Task<Country> Get(int Id);

        Task<bool> Create(Country country);
    }
}
