using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeService
    {
        #region grape
        Task<Result<IEnumerable<DataContract.Grape>>> GetGrapes();

        Task<Result<DataContract.Grape>> GetGrape(int Id);

        Task<Result> SaveGrape(DataContract.Grape grape, SaveType saveType);

        Task<Result> DeleteGrape(int id);
        #endregion

        #region colours
        Task<Result<IEnumerable<DataContract.GrapeColour>>> GetAllColours();

        Task<Result<DataContract.GrapeColour>> GetColour(int Id);

        Task<Result> SaveColour(DataContract.GrapeColour grapeColour, SaveType saveType);

        Task<Result> DeleteColour(int Id);
        #endregion
    }
}
