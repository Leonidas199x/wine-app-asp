using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeService
    {
        Task<Result<IEnumerable<GrapeColour>>> GetAllColours();

        Task<Result<GrapeColour>> GetColour(int Id);

        Task<Result> SaveColour(GrapeColour grapeColour, SaveType saveType);

        Task<Result> DeleteColour(int Id);
    }
}
