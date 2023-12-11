using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using Newtonsoft.Json;
using RealEstateApp.Core.Application.ViewModel.Provinces;
using Microsoft.AspNetCore.Authorization;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles ="Agent")]
    public class AgentController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;
        private readonly AuthenticationResponse? user;
        private readonly IRealEstateService _realEstateService;
        private readonly IImprovementService _improvementService;
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;
        private readonly ITypeOfSaleService _typeOfSaleService;


        public AgentController(IHttpContextAccessor contextAccessor, IUserService userService, IRealEstateService realEstateService,
            IImprovementService improvementService, ITypeOfRealEstateService typeOfRealEstateService, ITypeOfSaleService typeOfSaleService)
        {
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _userService = userService;
            _realEstateService = realEstateService;
            _improvementService = improvementService;
            _typeOfRealEstateService = typeOfRealEstateService;
            _typeOfSaleService = typeOfSaleService;
        }

        public async Task<IActionResult> Index()
        { 
            var realEstates = await _realEstateService.GetAllByAgent(user.Id);
            return View("Index", realEstates);
        }

        public async Task<IActionResult> IndexEstate()
        {
            var realEstates = await _realEstateService.GetAllByAgent(user.Id);
            return View("IndexEstate", realEstates);
        }

        #region Create
        private async Task<List<string>> GetProvicensAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.digital.gob.do/v1/territories/provinces");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var provinces = JsonConvert.DeserializeObject<ProvicensReponse>(content);
                        List<string> provincesName = new();
                        foreach (var item in provinces.Data)
                        {
                            provincesName.Add(item.Name);
                        }
                        return provincesName;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        public async Task<IActionResult> CreateRealState()
        {
            ViewBag.Improvements = await _improvementService.GetAll();
            ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
            ViewBag.TypeOfSale = await _typeOfSaleService.GetAll();
            ViewBag.Provinces = await GetProvicensAsync();

            return View(new SaveRealEstateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRealState(SaveRealEstateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    ViewBag.Improvements = await _improvementService.GetAll();
                    ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                    ViewBag.TypeOfSale = await _typeOfSaleService.GetAll();
                    ViewBag.Provinces = await GetProvicensAsync();

                    return View("CreateRealState", model);
                }
                if (model.Files.Count > 4)
                {
                    model.HasError = true;
                    model.Error = "SOLO PUEDES SUBIR UN MAX DE 4 IMAGENES PARA LA PROPIEDAD";
                    ViewBag.Improvements = await _improvementService.GetAll();
                    ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                    ViewBag.TypeOfSale = await _typeOfSaleService.GetAll();
                    ViewBag.Provinces = await GetProvicensAsync();
                    return View("CreateRealState", model);
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
                var agent = await _userService.GetByUserIdAysnc(user.Id);
                return View(agent);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(SaveUserViewModel model)
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

        #region EditRealEstate
        public async Task<IActionResult> EditRealEstate(int id)
        {
            try
            {
                var realEstate = await _realEstateService.GetById(id);
                ViewBag.Improvements = await _improvementService.GetImprovementsNotInRealEstate(id);
                ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                ViewBag.TypeOfSale = await _typeOfSaleService.GetAll();
                ViewBag.Provinces = await GetProvicensAsync();
                return View("CreateRealState", realEstate);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditRealEstate(SaveRealEstateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Improvements = await _improvementService.GetImprovementsNotInRealEstate(model.Id);
                    ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                    ViewBag.TypeOfSale = await _typeOfSaleService.GetAll();
                    ViewBag.Provinces = await GetProvicensAsync();
                    return View("CreateRealEstate", model);
                }

                var realestate = await _realEstateService.UpdateRealEstate(model, model.Id);

                if(realestate.HasError)
                {
                    realestate.HasError = model.HasError;
                    realestate.Error = model.Error;

                    ViewBag.Improvements = await _improvementService.GetImprovementsNotInRealEstate(model.Id);
                    ViewBag.TypeOfRealEstate = await _typeOfRealEstateService.GetAll();
                    ViewBag.TypeOfSale = await _typeOfSaleService.GetAll();
                    ViewBag.Provinces = await GetProvicensAsync();
                    return View("CreateRealEstate", model);

                }

                return RedirectToRoute(new { controller = "Agent", action = "IndexEstate" });

            }catch(Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion
        #region DeleteRealEstate
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            try
            {
                var realEstate = await _realEstateService.GetById(id);
                return View(realEstate);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRealEstatePost(int id)
        {
            try
            {
                await _realEstateService.Delete(id);
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
