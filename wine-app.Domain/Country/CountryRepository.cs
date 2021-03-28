using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public class CountryRepository : ICountryRepository
    {
        public async Task<IEnumerable<Country>> GetAll()
        {
            var countries = new List<Country>()
            {
                new Country
                {
                    Id = 1,
                    Name = "France",
                },
            };

            return countries;
        }
    }
}
