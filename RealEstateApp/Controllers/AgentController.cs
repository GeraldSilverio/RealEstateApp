using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse? user;
        private readonly IRealEstateService _realEstateService;
        private readonly IImprovementService _improvementService;
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;
        private readonly ITypeOfSaleService _typeOfSaleService;


        public AgentController(IHttpContextAccessor contextAccessor, IMapper mapper, IUserService userService, IRealEstateService realEstateService,
            IImprovementService improvementService, ITypeOfRealEstateService typeOfRealEstateService, ITypeOfSaleService typeOfSaleService)
        {
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _userService = userService;
            _realEstateService = realEstateService;
            _improvementService = improvementService;
            _typeOfRealEstateService = typeOfRealEstateService;
            _typeOfSaleService = typeOfSaleService;
        }

        public async Task<IActionResult> IndexEstate()
        {
            //Agregar los get correspondientes filtrando por el usuario en sesion      
            var realEstates = await _realEstateService.GetAll();
            return View("IndexEstate", realEstates);
        }

        #region Create
        public async Task<IActionResult> CreateRealState()
        {
            SaveRealEstateViewModel model = new()
            {
                Improvements = await _improvementService.GetAll(),
                TypeOfRealEstate = await _typeOfRealEstateService.GetAll(),
                TypeOfSale = await _typeOfSaleService.GetAll(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRealState(SaveRealEstateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Improvements = await _improvementService.GetAll();
                    model.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                    model.TypeOfSale = await _typeOfSaleService.GetAll();

                    return View("CreateRealState", model);
                }
                if (model.Files.Count > 4)
                {
                    model.HasError = true;
                    model.Error = "SOLO PUEDES SUBIR 4 IMAGENES DE LA PROPIEDAD";
                    model.Improvements = await _improvementService.GetAll();
                    model.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                    model.TypeOfSale = await _typeOfSaleService.GetAll();
                    return View("CreateRealState",model);
                }
                SaveRealEstateViewModel response = await _realEstateService.Add(model);
                return RedirectToAction("IndexEstate");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }



        #endregion

        #region My Profile

        public async Task<IActionResult> MyProfile()
        {
            try
            {
                var agent = _mapper.Map<UpdateUserRequest>(await _userService.GetByUserIdAysnc(user.Id));
                return View(agent);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
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
                model.ImageUser = _userService.UploadFile(model.File, model.Id, true, model.ImageUser);
                await _userService.UpdateAsync(model);
                return RedirectToAction("MyProfile");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion
    }


}
