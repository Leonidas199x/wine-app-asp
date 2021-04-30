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

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.Country>>>> GetAll(int page, int pageSize)
        {
            return await _countryRepository.GetAll(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<IEnumerable<DataContract.CountryLookup>>> GetLookup()
        {
            return await _countryRepository.GetLookup().ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Country>> Get(int id)
        {
            return await _countryRepository.Get(id).ConfigureAwait(false);
        }

        public async Task<Result> Save(DataContract.Country country, SaveType saveType)
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
