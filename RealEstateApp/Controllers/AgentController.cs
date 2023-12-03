using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse? user;

        public AgentController(IHttpContextAccessor contextAccessor, IMapper mapper, IUserService userService)
        {
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> MyProfile()
        {
            try
            {
                var agent =_mapper.Map<UpdateUserRequest>(await _userService.GetByUserIdAysnc(user.Id));
                return View(agent);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion
    }

        [HttpPost]
        public async Task<IActionResult> MyProfile(UpdateUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.ImageUser = _userService.UplpadFile(model.File, model.Id, true,model.ImageUser);
                await _userService.UpdateAsync(model);
                return RedirectToAction("MyProfile");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
