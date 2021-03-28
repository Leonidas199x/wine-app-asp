using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAll();
    }
}
