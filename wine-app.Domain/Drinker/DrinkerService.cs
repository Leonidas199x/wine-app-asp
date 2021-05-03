using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Drinker
{
    public class DrinkerService : IDrinkerService
    {
        private readonly IDrinkerRepository _drinkerRepository;

        public DrinkerService(IDrinkerRepository drinkerRepository)
        {
            _drinkerRepository = drinkerRepository;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.Drinker>>>> GetDrinkers(int page, int pageSize)
        {
            return await _drinkerRepository.GetDrinkers(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Drinker>> GetDrinker(int id)
        {
            return await _drinkerRepository.GetDrinker(id).ConfigureAwait(false);
        }

        public async Task<Result> Save(DataContract.Drinker drinker, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _drinkerRepository.Create(drinker).ConfigureAwait(false);
            }

            return await _drinkerRepository.Update(drinker).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            return await _drinkerRepository.Delete(id).ConfigureAwait(false);
        }
    }
}
