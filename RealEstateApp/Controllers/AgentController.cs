using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IImprovementService _improvementService;

        public AgentController(IRealEstateService realEstateService, IImprovementService improvementService)
        {
            _realEstateService = realEstateService;
            _improvementService = improvementService;

        }

        public async Task<IActionResult> Index()
        {

            ViewBag.Improvements = await _improvementService.GetAll();
            await _realEstateService.GetAll();
            return View("IndexEstate");
        }

        #region Create
        public IActionResult CreateRealState()
        {
            return View(new SaveRealEstateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRealState(SaveRealEstateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var response = await _realEstateService.Add(model);

                if (response.HasError)
                {
                    model.HasError = response.HasError;
                    model.Error = response.Error;
                    return View(model);
                }
                return RedirectToAction("IndexEstate");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion
    }


}
