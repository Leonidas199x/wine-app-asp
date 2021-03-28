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

        public async Task<Country> Get(int Id)
        {
            return await _countryRepository.Get(Id).ConfigureAwait(false);
        }

        public async Task<bool> Save(Country country)
        {
            return await _countryRepository.Create(country).ConfigureAwait(false);
        }
    }
}
