using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using wine_app.Domain;
using wine_app.Domain.Country;
using wine_app.Domain.MapBox;
using wine_app.Domain.Region;
using wine_app.Models.Region;

namespace wine_app.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly ICountryService _countryService;
        private readonly IMapper _regionMapper;
        private readonly IMapBoxService _mapBoxService;

        public RegionController(
            IRegionService regionService, 
            IMapper regionMapper, 
            ICountryService countryService,
            IMapBoxService mapBoxService)
        {
            _regionService = regionService;
            _countryService = countryService;
            _regionMapper = regionMapper;
            _mapBoxService = mapBoxService;
        }

        [HttpGet]
        public async Task<IActionResult> List(int currentPage = 1, int pageSize = 10)
        {
            var domainRegions = await _regionService
                .GetRegions(currentPage, pageSize)
                .ConfigureAwait(false);

            if(!domainRegions.IsSuccess)
            {
                return View(new Models.PagedList<IEnumerable<RegionViewModel>>());
            }

            var domainViewModel = _regionMapper
                .Map<Models.PagedList<IEnumerable<RegionViewModel>>>(domainRegions.Data);

            var countries = await _countryService.GetLookup().ConfigureAwait(false);

            foreach (var region in domainViewModel.Data)
            {
                region.Country = countries
                    .Data
                    .Where(x => x.Id == region.CountryId).FirstOrDefault().Name;
            }

            var viewModel = new Result<Models.PagedList<IEnumerable<RegionViewModel>>>
                (domainViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> View(int id, bool isSuccess = false)
        {
            var domainRegion = await _regionService.GetRegion(id).ConfigureAwait(false);
            var viewModel = _regionMapper.Map<RegionViewModel>(domainRegion.Data);
            var countries = await _countryService.GetLookup().ConfigureAwait(false);

            viewModel.Country = countries
                .Data
                .Where(x => x.Id == domainRegion.Data.CountryId)
                .FirstOrDefault()
                .Name;

            var mapBoxData = await _mapBoxService
                .GetGeoInfo(domainRegion.Data.Name)
                .ConfigureAwait(false);

            viewModel.Coordinates = mapBoxData.Data.Features.FirstOrDefault().Center;

            return View(new Result<RegionViewModel>(viewModel, isSuccess));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isSuccess = false)
        {
            var domainRegion = await _regionService.GetRegion(id).ConfigureAwait(false);
            var editableRegion = _regionMapper.Map<EditableRegionViewModel>(domainRegion.Data);

            editableRegion.Countries = await GetCountries().ConfigureAwait(false);

            return View(new Result<EditableRegionViewModel>(editableRegion, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Result<EditableRegionViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableRegionViewModel>(model.Data));
            }

            var domainRegion = _regionMapper.Map<DataContract.Region>(model.Data);

            var saveResult = await _regionService
                .Save(domainRegion, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("View", "Region", new { id = model.Data.Id, IsSuccess = true });
            }

            var viewModel = new Result<EditableRegionViewModel>
               (saveResult.IsSuccess, saveResult.Error, model.Data);

            viewModel.Data.Countries = await GetCountries().ConfigureAwait(false);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var viewModel = new EditableRegionViewModel
            {
                Countries = await GetCountries().ConfigureAwait(false)
            };

            return View(new Result<EditableRegionViewModel>(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Result<EditableRegionViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableRegionViewModel>(model.Data));
            }

            var domainCountry = _regionMapper.Map<DataContract.Region>(model.Data);

            var saveResult = await _regionService
                .Save(domainCountry, SaveType.Insert)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "Region", string.Empty);
            }

            var viewModel = new Result<EditableRegionViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            viewModel.Data.Countries = await GetCountries().ConfigureAwait(false);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _regionService.Delete(id).ConfigureAwait(false);
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

        private async Task<IEnumerable<SelectListItem>> GetCountries()
        {
            var countriesResult = await _countryService.GetLookup().ConfigureAwait(false);
            var countries = countriesResult.Data;

            var selectList = new List<SelectListItem>()
            {
                new SelectListItem(),
            };

            foreach (var country in countries)
            {
                var listItem = new SelectListItem
                {
                    Value = country.Id.ToString(),
                    Text = country.Name,
                };

                selectList.Add(listItem);
            }

            return selectList;
        }
    }
}
