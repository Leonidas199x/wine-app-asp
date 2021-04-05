using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeRepository
    {
        Task<Result<IEnumerable<GrapeColour>>> GetAllColours();
    }
}
