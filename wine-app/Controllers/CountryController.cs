using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wine_app.Domain.Country;
using AutoMapper;
using System.Collections.Generic;
using wine_app.Models.Country;

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
            var domainCountries = await _countryService.GetAll().ConfigureAwait(false);

            return View(_countryMapper.Map<IEnumerable<Models.Country.Country>>(domainCountries));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var domainCountry = await _countryService.Get(Id).ConfigureAwait(false);

            return View(_countryMapper.Map<EditableCountryViewModel>(domainCountry));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditableCountryViewModel model)
        {
            var domainCountry = _countryMapper.Map<Domain.Country.Country>(model);

            var saveResult = await _countryService.Save(domainCountry, Domain.SaveType.Update).ConfigureAwait(false);
            if (saveResult)
            {
                return RedirectToAction("Edit", "Country", new { id = model.Id });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new EditableCountryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EditableCountryViewModel model)
        {
            var domainCountry = _countryMapper.Map<Domain.Country.Country>(model);

            var saveResult = await _countryService.Save(domainCountry, Domain.SaveType.Insert).ConfigureAwait(false);
            if(saveResult)
            {
                return RedirectToAction("List", "Country", string.Empty);
            }

            return View();
        }

        public async Task Delete(int Id)
        {
            await _countryService.Delete(Id).ConfigureAwait(false);
        }
    }
}
