using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wine_app.Domain;
using wine_app.Domain.WineType;
using wine_app.Models.WineType;

namespace wine_app.Controllers
{
    public class WineTypeController : Controller
    {
        private readonly IWineTypeService _wineTypeService;
        private readonly IMapper _mapper;

        public WineTypeController(IWineTypeService wineTypeService, IMapper mapper)
        {
            _wineTypeService = wineTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List(int currentPage = 1, int pageSize = 10)
        {
            var wineTypeResult = await _wineTypeService
                .GetWineTypes(currentPage, pageSize)
                .ConfigureAwait(false);

            var outboundWineType = _mapper.Map
                <Models.PagedList<IEnumerable<WineType>>>(wineTypeResult.Data);

            var viewModel = new Result<Models.PagedList<IEnumerable<WineType>>>
                (wineTypeResult.IsSuccess, wineTypeResult.Error, outboundWineType);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isSuccess = false)
        {
            var wineTypeResult = await _wineTypeService.Get(id).ConfigureAwait(false);
            var outboundWineType = _mapper.Map<EditableWineTypeViewModel>(wineTypeResult.Data);

            return View(new Result<EditableWineTypeViewModel>(outboundWineType, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Result<EditableWineTypeViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableWineTypeViewModel>(model.Data));
            }

            var domainWineType = _mapper.Map<DataContract.WineType>(model.Data);

            var saveResult = await _wineTypeService.
                Save(domainWineType, SaveType.Update)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "WineType",
                    new
                    {
                        id = model.Data.Id,
                        IsSuccess = true
                    });
            }

            var viewModel = new Result<EditableWineTypeViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new Result<EditableWineTypeViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Result<EditableWineTypeViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableWineTypeViewModel>(model.Data));
            }

            var domainWineType = _mapper.Map<DataContract.WineType>(model.Data);

            var saveResult = await _wineTypeService
                .Save(domainWineType, SaveType.Insert)
                .ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "WineType", string.Empty);
            }

            var viewModel = new Result<EditableWineTypeViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _wineTypeService.Delete(id).ConfigureAwait(false);
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
