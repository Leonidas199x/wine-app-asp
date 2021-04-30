using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.Region>>>> GetRegions(int page, int pageSize)
        {
            return await _regionRepository.GetRegions(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Region>> GetRegion(int id)
        {
            return await _regionRepository.GetRegion(id).ConfigureAwait(false);
        }

        public async Task<Result> Save(DataContract.Region region, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _regionRepository.Create(region).ConfigureAwait(false);
            }

            return await _regionRepository.Update(region).ConfigureAwait(false);
        }

        public async Task<Result> Delete(int id)
        {
            return await _regionRepository.Delete(id).ConfigureAwait(false);
        }
    }
}
