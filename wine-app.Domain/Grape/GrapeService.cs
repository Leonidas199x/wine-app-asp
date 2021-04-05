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

        public async Task<Result<IEnumerable<GrapeColour>>> GetAllColours()
        {
            return await _grapeRepository.GetAllColours().ConfigureAwait(false);
        }

        public async Task<Result<GrapeColour>> GetColour(int Id)
        {
            return await _grapeRepository.GetColour(Id).ConfigureAwait(false);
        }

        public async Task<Result> SaveColour(GrapeColour grapeColour, SaveType saveType)
        {
            if (saveType == SaveType.Insert)
            {
                return await _grapeRepository.CreateColour(grapeColour).ConfigureAwait(false);
            }

            return await _grapeRepository.UpdateColour(grapeColour).ConfigureAwait(false);
        }

        public async Task<Result> DeleteColour(int Id)
        {
            return await _grapeRepository.DeleteColour(Id).ConfigureAwait(false);
        }
    }
}
