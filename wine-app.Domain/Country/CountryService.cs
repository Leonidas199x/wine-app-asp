using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Country
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _countryRepository.GetAll().ConfigureAwait(false);
        }
    }
}
