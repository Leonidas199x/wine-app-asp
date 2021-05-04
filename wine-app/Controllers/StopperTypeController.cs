using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wine_app.Domain;
using wine_app.Domain.StopperType;
using wine_app.Models.StopperType;

namespace wine_app.Controllers
{
    public class StopperTypeController : Controller
    {
        private readonly IStopperTypeService _stopperTypeService;
        private readonly IMapper _mapper;

        public StopperTypeController(IStopperTypeService stopperTypeService, IMapper mapper)
        {
            _stopperTypeService = stopperTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List(int currentPage = 1, int pageSize = 10)
        {
            var stopperTypeResult = await _stopperTypeService
                .GetStopperTypes(currentPage, pageSize)
                .ConfigureAwait(false);

            var outboundDrinkers = _mapper.Map
                <Models.PagedList<IEnumerable<StopperType>>>(stopperTypeResult.Data);

            var viewModel = new Result<Models.PagedList<IEnumerable<StopperType>>>
                (stopperTypeResult.IsSuccess, stopperTypeResult.Error, outboundDrinkers);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isSuccess = false)
        {
            var domainStopper = await _stopperTypeService.Get(id).ConfigureAwait(false);
            var outboundStopper = _mapper.Map<EditableStopperTypeViewModel>(domainStopper.Data);

            return View(new Result<EditableStopperTypeViewModel>(outboundStopper, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Result<EditableStopperTypeViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableStopperTypeViewModel>(model.Data));
            }

            var domainStopper = _mapper.Map<DataContract.StopperType>(model.Data);

            var saveResult = await _stopperTypeService.
                Save(domainStopper, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "StopperType",
                    new
                    {
                        id = model.Data.Id,
                        IsSuccess = true
                    });
            }

            var viewModel = new Result<EditableStopperTypeViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new Result<EditableStopperTypeViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Result<EditableStopperTypeViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableStopperTypeViewModel>(model.Data));
            }

            var domainStopper = _mapper.Map<DataContract.StopperType>(model.Data);

            var saveResult = await _stopperTypeService
                .Save(domainStopper, SaveType.Insert)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "StopperType", string.Empty);
            }

            var viewModel = new Result<EditableStopperTypeViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stopperTypeService.Delete(id).ConfigureAwait(false);
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
