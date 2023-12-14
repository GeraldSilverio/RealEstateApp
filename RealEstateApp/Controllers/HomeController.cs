using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Home;
using Microsoft.AspNetCore.Authorization;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.Enums;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IRealEstateService _realEstateService;
        private readonly IUserService _userService;
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;
        public HomeController(IAgentService agentService, IRealEstateService realEstateService, IHttpContextAccessor contextAccessor, IUserService userService, ITypeOfRealEstateService typeOfRealEstateService)
        {
            _agentService = agentService;
            _realEstateService = realEstateService;
            _userService = userService;
            _typeOfRealEstateService = typeOfRealEstateService;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var homeView = new HomeViewModel()
            {
                RealStatesRegistered = _realEstateService.CountRealState(),
                ActiveAgents = await _userService.CountUser(true, Roles.Agent.ToString()),
                InActiveAgents = await _userService.CountUser(false, Roles.Agent.ToString()),
                ActiveDevelopers = await _userService.CountUser(true, Roles.Developer.ToString()),
                InActiveDevelopers = await _userService.CountUser(false, Roles.Developer.ToString()),
                ActiveClients = await _userService.CountUser(true, Roles.Client.ToString()),
                InActiveClients = await _userService.CountUser(false, Roles.Client.ToString()),
            };
            return View(homeView);
        }

        public async Task<IActionResult> PrincipalView()
        {
            ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
            var realEstates = await _realEstateService.GetAll();
            return View("PrincipalView", realEstates);
        }
        [HttpPost]
        public async Task<IActionResult> PrincipalView(string name, int? toilets, int? bedrooms, decimal? minPrice, decimal? maxPrice, string code)
        {
            try
            {
                ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                var realEstates = await _realEstateService.GetAllWithFilters(name, toilets, bedrooms, minPrice, maxPrice,code);
                return View(realEstates);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            var realEstate = await _realEstateService.GetRealEstateViewModelById(id);
            return View(realEstate);
        }

        public async Task<IActionResult> AgentList()
        {
            try
            {
                var agents = await _agentService.GetAllWithFilterAsync(null);
                return View(agents);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AgentList(string name)
        {
            try
            {
                var agents = await _agentService.GetAllWithFilterAsync(name);
                return View(agents);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> AgentRealEstates(string id)
        {
            try
            {
                var realEstates = await _realEstateService.GetAllByAgent(id);
                return View(realEstates);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
       

        #region ChangePassword
        [Authorize(Roles = "Agent,Client")]
        public IActionResult ChangePasswordUser(string id)
        {
            var change = new ChangePasswordViewModel
            {
                Id = id
            };
            return View("ChangePassworUser", change);
        }

        [Authorize(Roles = "Agent,Client")]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordUser(ChangePasswordViewModel model, bool rolClient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("ChangePassworUser", model);
                }

                var isCheck = await _userService.CheckOldPassword(model.OldPassword, model.Id);

                if (!isCheck)
                {
                    model.HasError = true;
                    model.Error = "La contraseña no coincide";
                    return View("ChangePassworUser", model);
                }

                await _userService.ChangePasswordAsync(model);

                return View("ChangePassword");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        #endregion


    }
}