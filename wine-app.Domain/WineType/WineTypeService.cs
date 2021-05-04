using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.WineType
{
    public class WineTypeService : IWineTypeService
    {
        private readonly IWineTypeRepository _wineTypeRepository;

        public WineTypeService(IWineTypeRepository wineTypeRepository)
        {
            _wineTypeRepository = wineTypeRepository;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.WineType>>>> GetWineTypes(int page, int pageSize)
        {
            return await _wineTypeRepository.GetWineTypes(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.WineType>> Get(int id)
        {
            return await _wineTypeRepository.Get(id).ConfigureAwait(false);
        }

        public async Task<Result> Save(DataContract.WineType wineType, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _wineTypeRepository.Create(wineType).ConfigureAwait(false);
            }

            return await _wineTypeRepository.Update(wineType).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            return await _wineTypeRepository.Delete(id).ConfigureAwait(false);
        }
    }
}
