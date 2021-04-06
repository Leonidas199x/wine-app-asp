using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using wine_app.Domain;
using wine_app.Domain.Grape;
using wine_app.Models.Grape;

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

        [HttpGet]
        public async Task<IActionResult> EditColour(int Id, bool isSuccess = false)
        {
            var domainGrapeColour = await _grapeService.GetColour(Id).ConfigureAwait(false);
            var outboundGrapeColour = _grapeMapper.Map<EditableGrapeColourViewModel>(domainGrapeColour.Data);

            return View(new Result<EditableGrapeColourViewModel>(outboundGrapeColour, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> EditColour(Result<EditableGrapeColourViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableGrapeColourViewModel>(model.Data));
            }

            var domainGrapeColour = _grapeMapper.Map<Domain.Grape.GrapeColour>(model.Data);

            var saveResult = await _grapeService.SaveColour(domainGrapeColour, SaveType.Update).ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("EditColour", "Grape", new { id = model.Data.Id, IsSuccess = true });
            }

            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteColour(int Id)
        {
            var result = await _grapeService.DeleteColour(Id).ConfigureAwait(false);
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
