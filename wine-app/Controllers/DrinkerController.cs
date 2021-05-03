using System.Collections.Generic;
using System.Net;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isSuccess = false)
        {
            var domainDrinker = await _drinkerService.GetDrinker(id).ConfigureAwait(false);
            var outboundDrinker = _mapper.Map<EditableDrinkerViewModel>(domainDrinker.Data);

            return View(new Result<EditableDrinkerViewModel>(outboundDrinker, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Result<EditableDrinkerViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableDrinkerViewModel>(model.Data));
            }

            var domainDrinker = _mapper.Map<DataContract.Drinker>(model.Data);

            var saveResult = await _drinkerService.
                Save(domainDrinker, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "Drinker", 
                    new 
                    { 
                        id = model.Data.Id, 
                        IsSuccess = true 
                    });
            }

            var viewModel = new Result<EditableDrinkerViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new Result<EditableDrinkerViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Result<EditableDrinkerViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableDrinkerViewModel>(model.Data));
            }

            var domainDrinker = _mapper.Map<DataContract.Drinker>(model.Data);

            var saveResult = await _drinkerService
                .Save(domainDrinker, SaveType.Insert)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "Drinker", string.Empty);
            }

            var viewModel = new Result<EditableDrinkerViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _drinkerService.Delete(id).ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return Ok();
            }

            switch (result.HttpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.InternalServerError:
                    return StatusCode(500);
                default:
                    return BadRequest();
            }
        }
    }
}
