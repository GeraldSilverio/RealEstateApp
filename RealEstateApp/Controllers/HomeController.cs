using Microsoft.AspNetCore.Mvc;
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
        public HomeController(IAdminService adminService, IAccountService accountService, IAgentService agentService, IRealEstateService realEstateService)
        {
            _agentService = agentService;
            _realEstateService = realEstateService;
            _accountService = accountService;
            _adminService = adminService;
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

    }
}