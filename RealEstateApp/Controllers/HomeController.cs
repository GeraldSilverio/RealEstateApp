﻿using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IRealEstateService _realEstateService;
        public HomeController(IAgentService agentService, IRealEstateService realEstateService)
        {
            _agentService = agentService;
            _realEstateService = realEstateService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrincipalView()
        {
            return View();
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