﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace wine_app.Domain.Region
{
    public interface IRegionService
    {
        Task<Result<PagedList<IEnumerable<Region>>>> GetRegions(int page, int pageSize);

        Task<Result<Region>> GetRegion(int id);

        Task<Result> Save(Region region, SaveType saveType);

        Task<Result> Delete(int id);
    }
}