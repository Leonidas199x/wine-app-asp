using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wine_app.Domain.Country;
using AutoMapper;
using System.Collections.Generic;
using wine_app.Models.Country;
using wine_app.Domain;
using System.Net;
using wine_app.Domain.MapBox;
using System.Linq;

namespace wine_app.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _countryMapper;
        private readonly IMapBoxService _mapBoxService;

        public CountryController(
            ICountryService countryService, 
            IMapper countryMapper, 
            IMapBoxService mapBoxService)
        {
            _countryService = countryService;
            _countryMapper = countryMapper;
            _mapBoxService = mapBoxService;
        }

        [HttpGet]
        public async Task<IActionResult> List(int currentPage = 1, int pageSize= 10)
        {
            var countriesResult = await _countryService.GetAll(currentPage, pageSize)
                .ConfigureAwait(false);

            var outboundCountries = _countryMapper
                .Map<Models.PagedList<IEnumerable<Country>>>(countriesResult.Data);

            var viewModel = new Result<Models.PagedList<IEnumerable<Country>>>
                (countriesResult.IsSuccess, countriesResult.Error, outboundCountries);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> View(int id, bool isSuccess = false)
        {
            var domainCountry = await _countryService.Get(id).ConfigureAwait(false);
            var viewModel = _countryMapper.Map<CountryViewModel>(domainCountry.Data);
            var mapBoxData = await _mapBoxService
                .GetGeoInfo(domainCountry.Data.Name)
                .ConfigureAwait(false);
            viewModel.Coordinates = mapBoxData.Data.Features.FirstOrDefault().Center;

            return View(new Result<CountryViewModel>(viewModel, isSuccess));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isSuccess = false)
        {
            var domainCountry = await _countryService.Get(id).ConfigureAwait(false);
            var outboundCountry = _countryMapper.Map<EditableCountryViewModel>(domainCountry.Data);

            return View(new Result<EditableCountryViewModel>(outboundCountry, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Result<EditableCountryViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableCountryViewModel>(model.Data));
            }

            var domainCountry = _countryMapper.Map<DataContract.Country >(model.Data);

            var saveResult = await _countryService.
                Save(domainCountry, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("View", "Country", new { id = model.Data.Id , IsSuccess = true });
            }

            var viewModel = new Result<EditableCountryViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new Result<EditableCountryViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Result<EditableCountryViewModel> model)
        {
            if(!ModelState.IsValid)
            {
                return View(new Result<EditableCountryViewModel>(model.Data));
            }

            var domainCountry = _countryMapper.Map<DataContract.Country>(model.Data);

            var saveResult = await _countryService
                .Save(domainCountry, SaveType.Insert)
                .ConfigureAwait(false);
            if(saveResult.IsSuccess)
            {
                return RedirectToAction("List", "Country", string.Empty);
            }

            var viewModel = new Result<EditableCountryViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _countryService.Delete(id).ConfigureAwait(false);
            if(result.IsSuccess)
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
