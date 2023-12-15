using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DeveloperController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public DeveloperController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllAsync(Roles.Developer.ToString()));
        }
        #region Create
        public IActionResult CreateDeveloper()
        {
            return View(new SaveUserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CreateDeveloper(SaveUserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var response = await _userService.RegisterAsync(model, null);
                if (response.HasError)
                {
                    model.HasError = response.HasError;
                    model.Error = response.Error;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        public async Task<IActionResult> EditDeveloper(string id)
        {
            try
            {
                var user = _mapper.Map<UpdateUserRequest>(await _userService.GetByUserIdAysnc(id));
                
                return View(user);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditDeveloper(UpdateUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var response = await _userService.UpdateAsync(_mapper.Map<SaveUserViewModel>(model));

                if (response.HasError)
                {
                    model.HasError = response.HasError;
                    model.Error = response.Error;
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> ActiveOrDesactive(string id)
        {
            try
            {
                var user = await _userService.GetByUserIdAysnc(id);
                return View(user);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ActiveOrDesactive(SaveUserViewModel model)
        {
            try
            {
                await _userService.ChangeStatusAsync(model.Id, model.IsActive);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

    }
}
