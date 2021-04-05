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
    }
}
