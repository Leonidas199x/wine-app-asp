using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wine_app.Domain;
using wine_app.Domain.Region;
using wine_app.Models.Region;

namespace wine_app.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IMapper _regionMapper;

        public RegionController(IRegionService regionService, IMapper regionMapper)
        {
            _regionService = regionService;
            _regionMapper = regionMapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var domainRegions = await _regionService.GetRegions().ConfigureAwait(false);
            var domainViewModel = _regionMapper.Map<IEnumerable<RegionViewModel>>(domainRegions.Data);
            var viewModel = new Result<IEnumerable<RegionViewModel>>(domainViewModel);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewRegion(int id, bool isSuccess = false)
        {
            var domainRegion = await _regionService.GetRegion(id).ConfigureAwait(false);
            var viewModel = _regionMapper.Map<RegionViewModel>(domainRegion.Data);

            return View(new Result<RegionViewModel>(viewModel, isSuccess));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isSuccess = false)
        {
            var domainRegion = await _regionService.GetRegion(id).ConfigureAwait(false);
            var outboundCountry = _regionMapper.Map<EditableRegionViewModel>(domainRegion.Data);

            return View(new Result<EditableRegionViewModel>(outboundCountry, isSuccess));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Result<EditableRegionViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableRegionViewModel>(model.Data));
            }

            var domainRegion = _regionMapper.Map<Region>(model.Data);

            var saveResult = await _regionService.Save(domainRegion, SaveType.Update).ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("Edit", "Region", new { id = model.Data.Id, IsSuccess = true });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View(new Result<EditableRegionViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Result<EditableRegionViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                return View(new Result<EditableRegionViewModel>(model.Data));
            }

            var domainCountry = _regionMapper.Map<Region>(model.Data);

            var saveResult = await _regionService.Save(domainCountry, SaveType.Insert).ConfigureAwait(false);
            if (saveResult.IsSuccess)
            {
                return RedirectToAction("List", "Region", string.Empty);
            }

            var viewModel = new Result<EditableRegionViewModel>
                (saveResult.IsSuccess, saveResult.Error, model.Data);

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
    }
}
