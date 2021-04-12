using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wine_app.Domain.Country;
using AutoMapper;
using System.Collections.Generic;
using wine_app.Models.Country;
using wine_app.Domain;
using System.Net;

namespace wine_app.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _countryMapper;

        public CountryController(ICountryService countryService, IMapper countryMapper)
        {
            _countryService = countryService;
            _countryMapper = countryMapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var countriesResult = await _countryService.GetAll().ConfigureAwait(false);
            var outboundCountries = _countryMapper
                .Map<IEnumerable<Models.Country.Country>>(countriesResult.Data);

            var viewModel = new Result<IEnumerable<Models.Country.Country>>
                (countriesResult.IsSuccess, countriesResult.Error, outboundCountries);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id, bool isSuccess = false)
        {
            var domainCountry = await _countryService.Get(Id).ConfigureAwait(false);
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

            var domainCountry = _countryMapper.Map<Domain.Country.Country>(model.Data);

            var saveResult = await _countryService.Save(domainCountry, SaveType.Update).ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("Edit", "Country", new { id = model.Data.Id , IsSuccess = true });
            }

            return View();
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

            var domainCountry = _countryMapper.Map<Domain.Country.Country>(model.Data);

            var saveResult = await _countryService.Save(domainCountry, SaveType.Insert).ConfigureAwait(false);
            if(saveResult.IsSuccess)
            {
                return RedirectToAction("List", "Country", string.Empty);
            }

            var viewModel = new Result<EditableCountryViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _countryService.Delete(Id).ConfigureAwait(false);
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
