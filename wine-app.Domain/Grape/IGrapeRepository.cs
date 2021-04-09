using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeRepository
    {
        Task<Result<IEnumerable<Grape>>> GetGrapes();

        Task<Result<Grape>> GetGrape(int Id);

        Task<Result<IEnumerable<GrapeColour>>> GetAllColours();

        Task<Result<GrapeColour>> GetColour(int id);

        Task<Result> CreateColour(GrapeColour grapeColour);

        Task<Result> UpdateColour(GrapeColour grapeColour);

        Task<Result> DeleteColour(int Id);
    }
}
