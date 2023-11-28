using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModel.User;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.RealEstateApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAgentService _agentService;

        public UserController(IUserService userService, IAgentService agentService)
        {
            _userService = userService;
            _agentService = agentService;
        }
        public async Task<IActionResult> UserAdministrator()
        {
            var users = await _userService.GetAllAsync();
            var admins = users.Where(x => x.Roles.Contains("Admin")).ToList();

            return View(admins);
        }

        public async Task<IActionResult> ListOfAgents()
        {
            return View(await _agentService.GetAllAgentAsync());
        }

        #region AddUsers
        public IActionResult AddUser()
        {
            return View(new SaveUserViewModel());
        }



        #endregion 

        //public IActionResult ResetPassword()
        //{
        //    return View(new ResetPasswordViewModel());
        //}
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var response = await _userService.ResetPasswordAsync(model);
        //    if (response.HasError)
        //    {
        //        model.HasError = response.HasError;
        //        model.Error = response.Error;
        //        return View(model);
        //    }
        //    return RedirectToRoute(new { controller = "User", action = "PasswordConfirm" });
        //}


        /*
        #region Active/Desactivate Users
        public async Task<ActionResult> DesactivateUser(string id)
        {
            UserStatusViewModel viewModel = await _userService.GetUserById(id);
            return View("ActiveOrDesactiveUser", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DesactivateUser(UserStatusViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }
                await _userService.UpdateStatus(vm.Id, false);
                return RedirectToAction("UserAdministrator");
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }

        public async Task<IActionResult> ActivateUser(string id)
        {
            UserStatusViewModel viewModel = await _userService.GetUserById(id);
            return View("ActiveOrDesactiveUser", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(UserStatusViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                await _userService.UpdateStatus(vm.Id, true);
                return RedirectToAction("UserAdministrator");
            }
            catch (Exception ex)
            {

                return View(ex.Message);
            }
        }

        #endregion
        */
    }
}
