using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Application.ViewModel.User;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateApp.Presentation.WebApp.Controllers
{

    [Authorize(Roles = "Admin")]
    public class TypeOfRealStateController : Controller
    {
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;

        public TypeOfRealStateController(ITypeOfRealEstateService typeOfRealEstateService)
        {
            _typeOfRealEstateService = typeOfRealEstateService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _typeOfRealEstateService.GetAll());
        }

        public async Task<ActionResult> Create()
        {
            return View(new SaveTypeOfRealStateViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(SaveTypeOfRealStateViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                await _typeOfRealEstateService.Add(vm);

                if (vm.HasError)
                {
                    return View(vm);
                }
                return RedirectToRoute(new { controller = "TypeOfRealState", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveTypeOfRealStateViewModel editUser = await _typeOfRealEstateService.GetById(id);
            return View("Create", editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveTypeOfRealStateViewModel vm)
        {
            try
            {
                await _typeOfRealEstateService.Update(vm, vm.Id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            SaveTypeOfRealStateViewModel editUser = await _typeOfRealEstateService.GetById(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SaveTypeOfRealStateViewModel vm)
        {
            try
            {
                await _typeOfRealEstateService.Delete(vm.Id);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
