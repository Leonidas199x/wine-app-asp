using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Grape
{
    public interface IGrapeRepository
    {
        #region grape
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Grape>>>> GetGrapes(int page, int pageSize);

        Task<Result<DataContract.Grape>> GetGrape(int id);

        Task<Result> CreateGrape(DataContract.Grape grape);

        Task<Result> UpdateGrape(DataContract.Grape grape);

        Task<Result> DeleteGrape(int id);
        #endregion

        #region GrapeColour
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.GrapeColour>>>> GetAllColours(int page, int pageSize);

        Task<Result<DataContract.GrapeColour>> GetColour(int id);

        Task<Result> CreateColour(DataContract.GrapeColour grapeColour);

        Task<Result> UpdateColour(DataContract.GrapeColour grapeColour);

        Task<Result> DeleteColour(int id);
        #endregion
    }
}
