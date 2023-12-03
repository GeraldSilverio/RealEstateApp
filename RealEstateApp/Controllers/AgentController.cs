using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

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

        public async Task<IActionResult> Index()
        {
            //Agregar los get correspondientes filtrando por el usuario en sesion      
            await _realEstateService.GetAll();
            return View("IndexEstate");
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
                    return View(model);
                }

                SaveRealEstateViewModel response = await _realEstateService.Add(model);

                //Agregar una referencia a los metodos Add tanto de las mejoras como de las imagene 
                //Y determinar donde seria mas practico agregar estas referencias, si en el servicio o en controlador mismo

                if(response != null && response.Id != 0)
                {
                    foreach(var file in model.Files)
                    {
                        string image = UploadFile(file, response.Id, response.IdAgent);
                    }              
                    await _realEstateService.Update(response, response.Id);
                }

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

        private string UploadFile(IFormFile file, int id, string idAgent, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string baseRoute = $"/Images/RealEstate/{idAgent}/{id}";
            string route = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{baseRoute}");

            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(route, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(route, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{baseRoute}/{fileName}";
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
