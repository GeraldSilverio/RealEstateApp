using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Home;

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
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;
        private readonly AuthenticationResponse? user;
        public HomeController(IAdminService adminService, IAccountService accountService, IAgentService agentService, IRealEstateService realEstateService, IRealEstateClientService realEstateClientService, IHttpContextAccessor contextAccessor, ITypeOfRealEstateService typeOfRealEstateService)
        {
            _agentService = agentService;
            _realEstateService = realEstateService;
            _accountService = accountService;
            _adminService = adminService;
            _realEstateClientService = realEstateClientService;
            _contextAccessor = contextAccessor;
            _typeOfRealEstateService = typeOfRealEstateService;
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
        public async Task<IActionResult> PrincipalView(string name, int? toilets, int? bedrooms, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var realEstates = await _realEstateService.GetAll();

                // Aplica los filtros de nombre, baños y habitaciones si se proporcionan
                if (!string.IsNullOrEmpty(name))
                {
                    realEstates = realEstates.Where(x => x.TypeOfRealEstateName == name).ToList();
                }

                if (toilets.HasValue)
                {
                    realEstates = realEstates.Where(x => x.BathRooms == toilets).ToList();
                }

                if (bedrooms.HasValue)
                {
                    realEstates = realEstates.Where(x => x.BedRooms == bedrooms).ToList();
                }

                // Aplica los filtros de precio mínimo y máximo si se proporcionan
                if (minPrice.HasValue)
                {
                    realEstates = realEstates.Where(x => x.Price >= minPrice).ToList();
                }

                if (maxPrice.HasValue)
                {
                    realEstates = realEstates.Where(x => x.Price <= maxPrice).ToList();
                }

                return View(realEstates);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}