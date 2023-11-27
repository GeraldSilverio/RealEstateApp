using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Improvement;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class ImprovementController : Controller
    {
        private readonly IImprovementService _improvementService;

        public ImprovementController(IImprovementService improvementService)
        {
            _improvementService = improvementService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _improvementService.GetAll());
        }

        public async Task<ActionResult> Create()
        {
            return View(new SaveImprovementViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(SaveImprovementViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                await _improvementService.Add(vm);

                if (vm.HasError)
                {
                    return View(vm);
                }
                return RedirectToRoute(new { controller = "Improvement", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveImprovementViewModel editUser = await _improvementService.GetById(id);
            return View("Create", editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveImprovementViewModel vm)
        {
            try
            {
                await _improvementService.Update(vm, vm.Id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            SaveImprovementViewModel editUser = await _improvementService.GetById(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveImprovementViewModel vm)
        {
            try
            {
                await _improvementService.Delete(vm.Id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
