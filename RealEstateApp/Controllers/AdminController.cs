using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUserService userService;

        public AdminController(IAdminService adminService, IAccountService accountService, IMapper mapper)
        {
            _adminService = adminService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<IActionResult> AdminView()
        {
            return View(await _adminService.GetAllAdmin());
        }

        public IActionResult CreateAdmin()
        {
            return View(new SaveUserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(SaveUserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var response = await _adminService.RegisterAdmin(model);
                if (response.HasError)
                {
                    model.HasError = response.HasError;
                    model.Error = response.Error;
                    return View(model);
                }
                return RedirectToAction("AdminView");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> EditAdmin(string id)
        {
            try
            {
                var admin = _mapper.Map<UpdateUserRequest>(await _accountService.GetUserByIdAsync(id));
                return View(admin);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(UpdateUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                   return View(model);
                }
                await _accountService.UpdateAsync(model, model.Id);
                return RedirectToAction("AdminView");
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
                var user = _mapper.Map<SaveUserViewModel>(await _accountService.GetUserByIdAsync(id));
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
                await _accountService.ChangeStatusAsync(model.Id, model.IsActive);
                return RedirectToAction("AdminView");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }



    }
}
