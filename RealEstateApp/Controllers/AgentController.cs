using RealEstateApp.Core.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using Newtonsoft.Json;
using RealEstateApp.Core.Application.ViewModel.Provinces;
using Microsoft.AspNetCore.Authorization;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.Facade;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse? user;
        private readonly FacadeForAgent _facadeForAgent;

        public AgentController(IHttpContextAccessor contextAccessor, FacadeForAgent facadeForAgent)
        {
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _facadeForAgent = facadeForAgent;
        }

        public async Task<IActionResult> IndexEstate()
        {
            var realEstates = await _facadeForAgent.GetAllByAgentInSession(user.Id);
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
            ViewBag.Improvements = await _facadeForAgent.GetAllImprovements();
            ViewBag.TypeOfRealEstate = await _facadeForAgent.GetAllTypeOfRealEstate();
            ViewBag.TypeOfSale = await _facadeForAgent.GetAllTypeOfSale();
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

                    ViewBag.Improvements = await _facadeForAgent.GetAllImprovements();
                    ViewBag.TypeOfRealEstate = await _facadeForAgent.GetAllTypeOfRealEstate();
                    ViewBag.TypeOfSale = await _facadeForAgent.GetAllTypeOfSale();
                    ViewBag.Provinces = await GetProvicensAsync();

                    return View("CreateRealState", model);
                }
                if (model.Files.Count > 4)
                {
                    model.HasError = true;
                    model.Error = "SOLO PUEDES SUBIR UN MAX DE 4 IMAGENES PARA LA PROPIEDAD";
                    ViewBag.Improvements = await _facadeForAgent.GetAllImprovements();
                    ViewBag.TypeOfRealEstate = await _facadeForAgent.GetAllTypeOfRealEstate();
                    ViewBag.TypeOfSale = await _facadeForAgent.GetAllTypeOfSale();
                    ViewBag.Provinces = await GetProvicensAsync();
                    return View("CreateRealState", model);
                }
                SaveRealEstateViewModel response = await _facadeForAgent.AddRealEstate(model);
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
                var agent = await _facadeForAgent.GetByAgentId(user.Id);
                return View(agent);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(MyProfileViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.ImageUser = _facadeForAgent.UploadFileAgent(model.File, model.Id, true, model.ImageUser);
                await _facadeForAgent.UpdateAgent(model);
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
                var realEstate = await _facadeForAgent.GetRealEstateById(id);
                ViewBag.Improvements = await _facadeForAgent.GetImprovementsRealEstate(id, false);
                ViewBag.TypeOfRealEstate = await _facadeForAgent.GetAllTypeOfRealEstate();
                ViewBag.TypeOfSale = await _facadeForAgent.GetAllTypeOfSale();
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
                    ViewBag.Improvements = await _facadeForAgent.GetImprovementsRealEstate(model.Id, false);
                    ViewBag.TypeOfRealEstate = await _facadeForAgent.GetAllTypeOfRealEstate();
                    ViewBag.TypeOfSale = await _facadeForAgent.GetAllTypeOfSale();
                    ViewBag.Provinces = await GetProvicensAsync();
                    return View("CreateRealEstate", model);
                }

                var realestate = await _facadeForAgent.UpdateRealEstate(model, model.Id);

                if (realestate.HasError)
                {
                    realestate.HasError = model.HasError;
                    realestate.Error = model.Error;

                    ViewBag.Improvements = await _facadeForAgent.GetImprovementsRealEstate(model.Id, false);
                    ViewBag.TypeOfRealEstate = await _facadeForAgent.GetAllTypeOfRealEstate();
                    ViewBag.TypeOfSale = await _facadeForAgent.GetAllTypeOfSale();
                    ViewBag.Provinces = await GetProvicensAsync();
                    return View("CreateRealEstate", model);

                }

                return RedirectToRoute(new { controller = "Agent", action = "IndexEstate" });

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #region ChangeImprovements

        public async Task<IActionResult> ChangeImprovements(int id)
        {
            try
            {

                if (id != 0)
                {
                    var realEstate = await _facadeForAgent.GetByIdRealEstateMap(id);
                    ViewBag.Improvements = await _facadeForAgent.GetImprovementsRealEstate(id, true);
                    return View(realEstate);
                }
                else
                {
                    var realEstateId = TempData["RealEstateId"];
                    id = Convert.ToInt32(realEstateId);

                    var realEstate = await _facadeForAgent.GetByIdRealEstateMap(id);
                    ViewBag.Improvements = await _facadeForAgent.GetImprovementsRealEstate(id, true);
                    return View(realEstate);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImprovement(int idImprovement, int idRealEstate)
        {
            try
            {
                await _facadeForAgent.DeleteOneImprovementInRealEstate(idImprovement, idRealEstate);

                TempData["RealEstateId"] = idRealEstate;

                return RedirectToRoute(new { controller = "Agent", action = "ChangeImprovements" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        #endregion


        #region ChangeImages

        public async Task<IActionResult> ChangeImages(int id)
        {
            try
            {

                if (id != 0)
                {
                    var realEstate = await _facadeForAgent.GetByIdRealEstateMap(id);
                    ViewBag.Images = await _facadeForAgent.GetAllImageByRealEstate(realEstate.Id);
                    return View(realEstate);
                }
                else
                {
                    var realEstateId = TempData["RealEstateId"];
                    id = Convert.ToInt32(realEstateId);

                    var realEstate = await _facadeForAgent.GetByIdRealEstateMap(id);
                    ViewBag.Images = await _facadeForAgent.GetAllImageByRealEstate(realEstate.Id);
                    return View(realEstate);
                }

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteImages(string image, int idRealEstate)
        {
            try
            {
                await _facadeForAgent.DeleteOneImageInRealEstate(image, idRealEstate);

                TempData["RealEstateId"] = idRealEstate;

                return RedirectToRoute(new { controller = "Agent", action = "ChangeImages" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion

        #endregion

        #region DeleteRealEstate
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            try
            {
                var realEstate = await _facadeForAgent.GetRealEstateById(id);
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
                await _facadeForAgent.DeleteRealEstate(id);
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
