using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Drinker
{
    public interface IDrinkerService
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Drinker>>>> GetDrinkers(int page, int pageSize);

        Task<Result<DataContract.Drinker>> GetDrinker(int id);

        Task<Result> Save(DataContract.Drinker drinker, SaveType saveType);

        Task<Result> Delete(int id);
    }
}