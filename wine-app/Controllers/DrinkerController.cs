using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wine_app.Domain;
using wine_app.Domain.Drinker;
using wine_app.Models.Drinker;

namespace wine_app.Controllers
{
    public class DrinkerController : Controller
    {
        private readonly IDrinkerService _drinkerService;
        private readonly IMapper _mapper;

        public DrinkerController(IDrinkerService drinkerSerivce, IMapper mapper)
        {
            _drinkerService = drinkerSerivce;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List(int currentPage = 1, int pageSize = 10)
        {
            var drinkersResult = await _drinkerService
                .GetDrinkers(currentPage, pageSize)
                .ConfigureAwait(false);

            var outboundDrinkers = _mapper.Map
                <Models.PagedList<IEnumerable<Drinker>>>(drinkersResult.Data);

            var viewModel = new Result<Models.PagedList<IEnumerable<Drinker>>>
                (drinkersResult.IsSuccess, drinkersResult.Error, outboundDrinkers);

            return View(viewModel);
        }
    }
}
