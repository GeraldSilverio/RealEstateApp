using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Presentation.WebApp.Middlewares;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userServices;
        private readonly IMapper _mapper;

        public LoginController(ILoginService loginService, IUserService userServices, IMapper mapper)
        {
            _loginService = loginService;
            _userServices = userServices;
            _mapper = mapper;
        }

        #region Login And Authorization
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        #region
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }

            AuthenticationResponse authenticationResponse = await _loginService.LoginAsync(vm);
            if (authenticationResponse != null && authenticationResponse.HasError == false)
            {
                HttpContext.Session.Set("user", authenticationResponse);
                if (authenticationResponse.Roles.Contains("Admin"))
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Client", action = "Index" });
                }

            }
            else
            {
                vm.HasError = authenticationResponse.HasError;
                vm.Error = authenticationResponse.Error;
                return View("Index", vm);
            }
        }
        #endregion

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveVM)
        {
            if (!ModelState.IsValid)
            {
                return View(saveVM);
            }
            var origin = Request.Headers["origin"];

            RegisterResponse response = await _loginService.RegisterAsync(saveVM, origin);

            if (response.HasError)
            {
                saveVM.HasError = response.HasError;
                saveVM.Error = response.Error;
                return View(saveVM);
            }

            saveVM.ImageUser = _userServices.UplpadFile(saveVM.File, response.IdUser);
            saveVM.Id = response.IdUser;
            await _userServices.UpdateAsync(_mapper.Map<UpdateUserRequest>(saveVM));

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        #endregion

        public async Task<IActionResult> LogOut()
        {
            await _loginService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _loginService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        #region ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var origin = Request.Headers["origin"];
            var response = await _userServices.ForgotPasswordAsync(model, origin);

            if (response.HasError)
            {
                model.HasError = response.HasError;
                model.Error = response.Error;
                return View(model);
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        #endregion
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }



    }
}
