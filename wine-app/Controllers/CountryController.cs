using Microsoft.AspNetCore.Mvc;

namespace wine_app.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
