using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypeOfSaleController : Controller
    {
        private readonly ITypeOfSaleService _typeOfSaleService;

        public TypeOfSaleController(ITypeOfSaleService typeOfSaleService)
        {
            _typeOfSaleService = typeOfSaleService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _typeOfSaleService.GetAll());
        }

        public async Task<ActionResult> Create()
        {
            return View(new SaveTypeOfSaleViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(SaveTypeOfSaleViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                await _typeOfSaleService.Add(vm);

                if (vm.HasError)
                {
                    return View(vm);
                }
                return RedirectToRoute(new { controller = "TypeOfSale", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveTypeOfSaleViewModel editUser = await _typeOfSaleService.GetById(id);
            return View("Create", editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveTypeOfSaleViewModel vm)
        {
            try
            {
                await _typeOfSaleService.Update(vm, vm.Id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            SaveTypeOfSaleViewModel editUser = await _typeOfSaleService.GetById(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveTypeOfSaleViewModel vm)
        {
            try
            {
                await _typeOfSaleService.Delete(vm.Id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
