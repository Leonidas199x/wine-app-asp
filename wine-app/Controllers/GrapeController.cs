using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using wine_app.Domain;
using wine_app.Domain.Grape;
using wine_app.Models.Grape;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        #region grape
        [HttpGet]
        public async Task<IActionResult> ListGrapes()
        {
            var domainGrapes = await _grapeService.GetGrapes().ConfigureAwait(false);
            var grapeViewModel = _grapeMapper.Map<IEnumerable<GrapeViewModel>>(domainGrapes.Data);
            var grapeColoursResult = await _grapeService.GetAllColours().ConfigureAwait(false);

            foreach(var grape in grapeViewModel)
            {
                if(grape.GrapeColourId != null)
                {
                    grape.GrapeColourString = grapeColoursResult
                    .Data
                    .Where(x => x.Id == grape.GrapeColourId.Value)
                    .FirstOrDefault()
                    .Colour;
                }
            }

            var viewModel = new Result<IEnumerable<GrapeViewModel>>
                (grapeColoursResult.IsSuccess, grapeColoursResult.Error, grapeViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> InsertGrape()
        {
            var viewModel = new EditableGrapeViewModel
            {
                GrapeColours = await GetGrapeColours().ConfigureAwait(false),
            };

            return View(new Result<EditableGrapeViewModel>(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> InsertGrape(Result<EditableGrapeViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                model.Data.GrapeColours = await GetGrapeColours().ConfigureAwait(false);
                return View(new Result<EditableGrapeViewModel>(model.Data));
            }

            var domainGrape = _grapeMapper.Map<DataContract.Grape>(model.Data);

            var saveResult = await _grapeService
                .SaveGrape(domainGrape, SaveType.Insert)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("ListGrapes", "Grape", string.Empty);
            }

            var viewModel = new Result<EditableGrapeViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            viewModel.Data.GrapeColours = await GetGrapeColours().ConfigureAwait(false);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditGrape(int id, bool isSuccess = false)
        {
            var domainGrape = await _grapeService.GetGrape(id).ConfigureAwait(false);
            var viewModel = _grapeMapper.Map<EditableGrapeViewModel>(domainGrape.Data);

            viewModel.GrapeColours = await GetGrapeColours().ConfigureAwait(false);

            return View(new Result<EditableGrapeViewModel>(viewModel, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> EditGrape(Result<EditableGrapeViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableGrapeViewModel>(model.Data));
            }

            var domainGrape = _grapeMapper.Map<DataContract.Grape>(model.Data);

            var saveResult = await _grapeService
                .SaveGrape(domainGrape, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("EditGrape", "Grape", new 
                { 
                    id = model.Data.Id, 
                    IsSuccess = true 
                });
            }

            var viewModel = new Result<EditableGrapeViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            viewModel.Data.GrapeColours = await GetGrapeColours().ConfigureAwait(false);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGrape(int id)
        {
            var result = await _grapeService.DeleteGrape(id).ConfigureAwait(false);
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
        #endregion

        #region colours
        [HttpGet]
        public async Task<IActionResult> ListColours()
        {
            var grapeColoursResult = await _grapeService.GetAllColours().ConfigureAwait(false);
            var outboundGrapeColours = _grapeMapper.Map<IEnumerable<Models.Grape.GrapeColour>>
                (grapeColoursResult.Data);

            var viewModel = new Result<IEnumerable<Models.Grape.GrapeColour>>
                (grapeColoursResult.IsSuccess, grapeColoursResult.Error, outboundGrapeColours);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditColour(int id, bool isSuccess = false)
        {
            var domainGrapeColour = await _grapeService.GetColour(id).ConfigureAwait(false);
            var outboundGrapeColour = _grapeMapper.Map<EditableGrapeColourViewModel>
                (domainGrapeColour.Data);

            return View(new Result<EditableGrapeColourViewModel>(outboundGrapeColour, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> EditColour(Result<EditableGrapeColourViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableGrapeColourViewModel>(model.Data));
            }

            var domainGrapeColour = _grapeMapper.Map<DataContract.GrapeColour>(model.Data);

            var saveResult = await _grapeService
                .SaveColour(domainGrapeColour, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("EditColour", "Grape", new 
                { 
                    id = model.Data.Id, 
                    IsSuccess = true 
                });
            }

            var viewModel = new Result<EditableGrapeColourViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult InsertColour()
        {
            return View(new Result<EditableGrapeColourViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> InsertColour(Result<EditableGrapeColourViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableGrapeColourViewModel>(model.Data));
            }

            var domainColour = _grapeMapper.Map<DataContract.GrapeColour>(model.Data);

            var saveResult = await _grapeService
                .SaveColour(domainColour, SaveType.Insert)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("ListColours", "Grape", string.Empty);
            }

            var viewModel = new Result<EditableGrapeColourViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteColour(int id)
        {
            var result = await _grapeService.DeleteColour(id).ConfigureAwait(false);
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
        #endregion

        private async Task<IEnumerable<SelectListItem>> GetGrapeColours()
        {
            var grapeColours = await _grapeService.GetAllColours().ConfigureAwait(false);

            var selectList = new List<SelectListItem>()
            {
                new SelectListItem(),
            };

            foreach (var colour in grapeColours.Data)
            {
                var listItem = new SelectListItem
                {
                    Value = colour.Id.ToString(),
                    Text = colour.Colour,
                };

                selectList.Add(listItem);
            }

            return selectList;
        }
    }
}
