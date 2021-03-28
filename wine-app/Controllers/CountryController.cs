using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using wine_app.Domain.Country;
using wine_app.Mappers.Country;

namespace wine_app.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> List()
        {
            var domainCountries = await _countryService.GetAll().ConfigureAwait(false);

            return View(CountryMapper.Map(domainCountries));
        }
    }
}
