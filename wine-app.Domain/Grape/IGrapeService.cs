using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeService
    {
        Task<Result<IEnumerable<GrapeColour>>> GetAllColours();
    }
}
