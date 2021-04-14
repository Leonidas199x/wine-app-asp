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

        public async Task<Result<IEnumerable<Country>>> GetAll()
        {
            return await _countryRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<Result<Country>> Get(int id)
        {
            return await _countryRepository.Get(id).ConfigureAwait(false);
        }

        public async Task<Result> Save(Country country, SaveType saveType)
        {
            if(saveType == SaveType.Insert)
            {
                return await _countryRepository.Create(country).ConfigureAwait(false);
            }

            return await _countryRepository.Update(country).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            return await _countryRepository.Delete(id).ConfigureAwait(false);
        }
    }
}
