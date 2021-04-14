using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeService
    {
        #region grape
        Task<Result<IEnumerable<Grape>>> GetGrapes();

        Task<Result<Grape>> GetGrape(int Id);

        Task<Result> SaveGrape(Grape grape, SaveType saveType);

        Task<Result> DeleteGrape(int id);
        #endregion

        #region colours
        Task<Result<IEnumerable<GrapeColour>>> GetAllColours();

        Task<Result<GrapeColour>> GetColour(int Id);

        Task<Result> SaveColour(GrapeColour grapeColour, SaveType saveType);

        Task<Result> DeleteColour(int Id);
        #endregion
    }
}
