using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }


}
