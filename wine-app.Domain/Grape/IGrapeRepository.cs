using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeRepository
    {
        #region grape
        Task<Result<IEnumerable<Grape>>> GetGrapes();

        Task<Result<Grape>> GetGrape(int Id);

        Task<Result> CreateGrape(Grape grape);

        Task<Result> UpdateGrape(Grape grape);
        #endregion

        #region GrapeColour
        Task<Result<IEnumerable<GrapeColour>>> GetAllColours();

        Task<Result<GrapeColour>> GetColour(int id);

        Task<Result> CreateColour(GrapeColour grapeColour);

        Task<Result> UpdateColour(GrapeColour grapeColour);

        Task<Result> DeleteColour(int Id);
        #endregion
    }
}
