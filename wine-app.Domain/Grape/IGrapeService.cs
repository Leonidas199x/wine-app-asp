using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeService
    {
        #region grape
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Grape>>>> GetGrapes(int page, int pageSize);

        Task<Result<DataContract.Grape>> GetGrape(int Id);

        Task<Result> SaveGrape(DataContract.Grape grape, SaveType saveType);

        Task<Result> DeleteGrape(int id);
        #endregion

        #region colours
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.GrapeColour>>>> GetAllColours(int page, int pageSize);

        Task<Result<DataContract.GrapeColour>> GetColour(int Id);

        Task<Result> SaveColour(DataContract.GrapeColour grapeColour, SaveType saveType);

        Task<Result> DeleteColour(int Id);
        #endregion
    }
}
