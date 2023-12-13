using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Home;
using RealEstateApp.Core.Application.Helpers;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IAccountService _accountService;
        private readonly IAdminService _adminService;
        private readonly IRealEstateService _realEstateService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRealEstateClientService _realEstateClientService;
        private readonly AuthenticationResponse? user;
        public HomeController(IAdminService adminService, IAccountService accountService, IAgentService agentService, IRealEstateService realEstateService, IRealEstateClientService realEstateClientService, IHttpContextAccessor contextAccessor)
        {
            _agentService = agentService;
            _realEstateService = realEstateService;
            _accountService = accountService;
            _adminService = adminService;
            _realEstateClientService = realEstateClientService;
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public IActionResult Index()
        {
            var agents = _agentService.GetAllAgentAsync();

            var homeView = new HomeViewModel()
            {
                RealStatesRegistered = _realEstateService.CountRealState(),
                ActiveAgents = agents.Result.Where(u => u.IsActive == true).Count(),
                InActiveAgents = agents.Result.Where(u => u.IsActive == false).Count(),
                ActiveDevelopers = _adminService.CountDevelopers(true),
                InActiveDevelopers = _adminService.CountDevelopers(false),
                ActiveClients = _accountService.CountClients(true),
                InActiveClients = _accountService.CountClients(false)
            };
            return View(homeView);
        }

        public async Task<IActionResult> PrincipalView()
        {
            #region Favorites
            if (user != null)
            {
                var favorites = await _realEstateClientService.GetFavoritesByUserId(user.Id);

                if (user.FavoritesRealEstate == null)
                {
                    user.FavoritesRealEstate = new List<int>();
                }

                foreach (var favorite in favorites)
                {
                    user.FavoritesRealEstate.Add(favorite.IdRealEstate);
                }
                _contextAccessor.HttpContext.Session.Set("user", user);
            }
            #endregion
            var realEstates = await _realEstateService.GetAll();
            return View("PrincipalView", realEstates);
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

        [HttpPost]
        public IActionResult PrincipalView(string name)
        {
            var realEstates = new List<RealEstateViewModel>();

            try
            {
                if (name != null)
                {
                    realEstates = _realEstateService.GetAll().Result.Where(x => x.TypeOfRealEstateName == name).ToList();
                }
                else
                {
                    return View(realEstates);
                }

                return View("PrincipalView", realEstates);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

    }
}