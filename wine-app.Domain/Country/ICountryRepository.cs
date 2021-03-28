using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAll();
    }
}
