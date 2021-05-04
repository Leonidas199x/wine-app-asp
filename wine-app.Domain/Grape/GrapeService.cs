using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public class GrapeService : IGrapeService
    {
        private IGrapeRepository _grapeRepository;

        public GrapeService(IGrapeRepository grapeRepository)
        {
            _grapeRepository = grapeRepository;
        }

        #region grape
        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.Grape>>>> GetGrapes(int page, int pageSize)
        {
            return await _grapeRepository.GetGrapes(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.Grape>> GetGrape(int id)
        {
            return await _grapeRepository.GetGrape(id).ConfigureAwait(false);
        }

        public async Task<Result> SaveGrape(DataContract.Grape grape, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _grapeRepository.CreateGrape(grape).ConfigureAwait(false);
            }

            return await _grapeRepository.UpdateGrape(grape).ConfigureAwait(false);
        }

        public async Task<Result> DeleteGrape(int id)
        {
            return await _grapeRepository.DeleteGrape(id).ConfigureAwait(false);
        }
        #endregion

        #region colours
        public async Task<Result<DataContract.PagedList<IEnumerable<DataContract.GrapeColour>>>> GetAllColours(int page, int pageSize)
        {
            return await _grapeRepository.GetAllColours(page, pageSize).ConfigureAwait(false);
        }

        public async Task<Result<DataContract.GrapeColour>> GetColour(int id)
        {
            return await _grapeRepository.GetColour(id).ConfigureAwait(false);
        }

        public async Task<Result> SaveColour(DataContract.GrapeColour grapeColour, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _grapeRepository.CreateColour(grapeColour).ConfigureAwait(false);
            }

            return await _grapeRepository.UpdateColour(grapeColour).ConfigureAwait(false);
        }

        public async Task<Result> DeleteColour(int id)
        {
            return await _grapeRepository.DeleteColour(id).ConfigureAwait(false);
        }
        #endregion
    }
}
