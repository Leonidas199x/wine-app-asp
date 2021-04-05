using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using wine_app.Domain;
using wine_app.Domain.Grape;

namespace wine_app.Controllers
{
    public class GrapeController : Controller
    {
        private readonly IGrapeService _grapeService;
        private readonly IMapper _grapeMapper;

        public GrapeController(IGrapeService grapeService, IMapper grapeMapper)
        {
            _grapeService = grapeService;
            _grapeMapper = grapeMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListColours()
        {
            var domainGrapeColours = await _grapeService.GetAllColours().ConfigureAwait(false);
            var outboundGrapeColours = _grapeMapper.Map<IEnumerable<Models.Grape.GrapeColour>>(domainGrapeColours.Data);

            return View(new Result<IEnumerable<Models.Grape.GrapeColour>>(outboundGrapeColours));
        }
    }
}
