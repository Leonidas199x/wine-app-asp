using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.StopperType
{
    public class StopperTypeService : IStopperTypeService
    {
        private readonly IStopperTypeRepository _stopperTypeRepository;

        public StopperTypeService(IStopperTypeRepository stopperTypeRepository)
        {
            _stopperTypeRepository = stopperTypeRepository;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.StopperType>>>> GetStopperTypes(int page, int pageSize)
        {
            return await _stopperTypeRepository.GetStopperTypes(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.StopperType>> Get(int id)
        {
            return await _stopperTypeRepository.Get(id).ConfigureAwait(false);
        }

        public async Task<Result> Save(DataContract.StopperType stopperType, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _stopperTypeRepository.Create(stopperType).ConfigureAwait(false);
            }

            return await _stopperTypeRepository.Update(stopperType).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            return await _stopperTypeRepository.Delete(id).ConfigureAwait(false);
        }
    }
}
