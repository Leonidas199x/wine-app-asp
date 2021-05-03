using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Drinker
{
    public interface IDrinkerRepository
    {
        Task<Result<DataContract.PagedList<IEnumerable<DataContract.Drinker>>>> GetDrinkers(int page, int pageSize);

        Task<Result<DataContract.Drinker>> GetDrinker(int id);

        Task<Result> Create(DataContract.Drinker drinker);

        Task<Result> Update(DataContract.Drinker drinker);

        Task<Result> Delete(int id);
    }
}