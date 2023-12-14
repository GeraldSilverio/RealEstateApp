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
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<IActionResult> AdminView()
        {
            return View(await _userService.GetAllAsync(Roles.Admin.ToString()));
        }

        #region Create
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
                var response = await _userService.RegisterAsync(model, null);
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
        #endregion

        #region Update
        public async Task<IActionResult> EditAdmin(string id)
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
        public async Task<IActionResult> EditAdmin(UpdateUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                await _userService.UpdateAsync(_mapper.Map<SaveUserViewModel>(model));
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
                return RedirectToAction("AdminView");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region ChangePassword
        public IActionResult ChangePassword(string id)
        {
            var change = new ChangePasswordViewModel
            {
                Id = id
            };
            return View(change);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                await _userService.ChangePasswordAsync(model);
                return RedirectToRoute(new { controller="Admin", action= "AdminView" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion

        #region AgentMethods
        public async Task<IActionResult> AgentList()
        {
            return View(await _userService.GetAllAsync(Roles.Agent.ToString()));
        }

        public async Task<IActionResult> DeleteAgent(string id)
        {
            var agent = await _userService.GetByUserIdAysnc(id);
            return View(agent);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAgent(SaveUserViewModel model)
        {
            try
            {
                //Poner aqui el metodo 
                await _userService.DeleteAsync(model.Id);
                return RedirectToAction("AgentList");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion

    }
}
