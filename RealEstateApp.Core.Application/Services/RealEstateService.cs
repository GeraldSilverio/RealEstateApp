using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.RealEstateImage;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Domain.Entities;
using System.Xml;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateService : GenericService<RealEstate, SaveRealEstateViewModel, RealEstateViewModel>, IRealEstateService
    {
        private readonly IRealEstateImageService _realEstateImageService;
        private readonly IRealEstateImprovementService _realEstateImprovementService;
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse User;
        public RealEstateService(IRealEstateRepository realEstateRepository, IMapper mapper, IRealEstateImageService realEstateImageService, IRealEstateImprovementService realEstateImprovementService,
            IUserService userService, IHttpContextAccessor httpContextAccessor) : base(realEstateRepository, mapper)
        {
            _realEstateImageService = realEstateImageService;
            _realEstateImprovementService = realEstateImprovementService;
            _realEstateRepository = realEstateRepository;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveRealEstateViewModel> Add(SaveRealEstateViewModel model)
        {
            //Creando y recuperando la propiedad recien creada.
           
            model.Code = GenerateCode.GenerateAccountCode(DateTime.Now);
            var realEstate = await base.Add(model);
            if (realEstate is null)
            {
                model.HasError = true;
                model.Error = "Ocurrio un error al crear la propiedad";
                return model;
            }
            //Agregando las imagenes.
            foreach (var image in model.Files)
            {
                var realEstateImage = new SaveRealEstateImageViewModel()
                {
                    IdRealEstate = realEstate.Id,
                    Image = _realEstateImageService.UploadFile(image, realEstate.Id, false, null)
                };
                await _realEstateImageService.Add(realEstateImage);
            }
            //Agregando las mejoras.
            foreach (var improvement in model.PropertiesImprovementsId)
            {
                var realEstateImprovement = new SaveRealEstateImprovementViewModeL()
                {
                    IdImprovement = improvement,
                    IdRealEstate = realEstate.Id
                };
                await _realEstateImprovementService.Add(realEstateImprovement);
            }
            return realEstate;
        }

        public override async Task Update(SaveRealEstateViewModel model, int id)
        {        
           await base .Update(model, id);
        }

        public async Task<SaveRealEstateViewModel> UpdateRealEstate(SaveRealEstateViewModel model, int id)
        {
            var code = await _realEstateRepository.GetByIdAsync(id);
            model.Code = code.Code;
            await base.Update(model, id);

            //Agregando nuevas imagenes
            if (model.Files != null)
            {
                int imagesCount = model.Files.Count;
                var imagesExists = await _realEstateImageService.GetImagesByRealEstateId(id);
                int imagesTotal = imagesCount + imagesExists.Count;

                if (imagesTotal <= 4)
                {
                    foreach (var image in model.Files)
                    {
                        var realEstateImage = new SaveRealEstateImageViewModel()
                        {
                            IdRealEstate = id,
                            Image = _realEstateImageService.UploadFile(image, id, false, null)
                        };
                        await _realEstateImageService.Add(realEstateImage);
                    }
                }
                else
                {
                    model.Error = "A LA PROPIEDAD NO SE LE PUEDEN AGREGAR MAS DE 4 IMAGENES";
                    model.HasError = true;
                }
            }

            //Agregando nuevas mejoras
            if (model.PropertiesImprovementsId != null)
            {
                foreach (var improvement in model.PropertiesImprovementsId)
                {
                    var realEstateImprovement = new SaveRealEstateImprovementViewModeL()
                    {
                        IdImprovement = improvement,
                        IdRealEstate = id
                    };
                    await _realEstateImprovementService.Add(realEstateImprovement);
                }
            }

            return model;
        }

        public override async Task<List<RealEstateViewModel>> GetAll()
        {
            var realEstateList = new List<RealEstateViewModel>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> {"TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });

            foreach(var realEstate in realEstates)
            {
                var user = await _userService.GetByUserIdAysnc(realEstate.IdAgent);
                var realEstateView = new RealEstateViewModel()
                {
                    Id = realEstate.Id,
                    Description = realEstate.Description,
                    BathRooms = realEstate.BathRooms,
                    BedRooms = realEstate.BedRooms,
                    Size = realEstate.Size,
                    Code = realEstate.Code,
                    IdAgent = realEstate.IdAgent,
                    Name = user.FirstName +" " + user.LastName,
                    Phone = user.PhoneNumber,
                    UserImage = user.ImageUser,
                    Email = user.Email,
                    Price = realEstate.Price,
                    Address = realEstate.Address,
                    TypeOfRealEstateName = realEstate.TypeOfRealEstate.Name,
                    TypeOfSaleName = realEstate.TypeOfSale.Name,
                    Images = await _realEstateImageService.GetImagesByRealEstateId(realEstate.Id),
                    ImprovementName = realEstate.RealEstateImprovements.Select(x=> x.Improvement.Name).ToList(),
                };
                realEstateList.Add(realEstateView);
            }
            return realEstateList;
        }

        public async Task<List<RealEstateViewModel>> GetAllByAgent(string idUser)
        {
            var realEstateList = new List<RealEstateViewModel>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });

            foreach (var realEstate in realEstates.Where(x=> x.IdAgent == idUser))
            {
                var user = await _userService.GetByUserIdAysnc(realEstate.IdAgent);
                var realEstateView = new RealEstateViewModel()
                {
                    Id = realEstate.Id,
                    Description = realEstate.Description,
                    BathRooms = realEstate.BathRooms,
                    BedRooms = realEstate.BedRooms,
                    Size = realEstate.Size,
                    Code = realEstate.Code,
                    IdAgent = realEstate.IdAgent,
                    Name = user.FirstName + " " + user.LastName,
                    Phone = user.PhoneNumber,
                    UserImage = user.ImageUser,
                    Email = user.Email,
                    Price = realEstate.Price,
                    Address = realEstate.Address,
                    TypeOfRealEstateName = realEstate.TypeOfRealEstate.Name,
                    TypeOfSaleName = realEstate.TypeOfSale.Name,
                    Images = await _realEstateImageService.GetImagesByRealEstateId(realEstate.Id),
                    ImprovementName = realEstate.RealEstateImprovements.Select(x => x.Improvement.Name).ToList(),
                };
                realEstateList.Add(realEstateView);
            }
            return realEstateList;
        }

        public async Task<RealEstateViewModel> GetRealEstateViewModelById(int id)
        {
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });
            var realEstate = realEstates.Find(x => x.Id == id);
            var user = await _userService.GetByUserIdAysnc(realEstate.IdAgent);
            var realEstateView = new RealEstateViewModel()
            {
                Id = realEstate.Id,
                Description = realEstate.Description,
                BathRooms = realEstate.BathRooms,
                BedRooms = realEstate.BedRooms,
                Size = realEstate.Size,
                Code = realEstate.Code,
                IdAgent = realEstate.IdAgent,
                Name = user.FirstName + " " + user.LastName,
                Phone = user.PhoneNumber,
                UserImage = user.ImageUser,
                Email = user.Email,
                Price = realEstate.Price,
                Address = realEstate.Address,
                TypeOfRealEstateName = realEstate.TypeOfRealEstate.Name,
                TypeOfSaleName = realEstate.TypeOfSale.Name,
                Images = await _realEstateImageService.GetImagesByRealEstateId(realEstate.Id),
                ImprovementIds = realEstate.RealEstateImprovements.Select(x => x.Improvement.Id).ToList(),
                ImprovementName = realEstate.RealEstateImprovements.Select(x => x.Improvement.Name).ToList()
            };
            return realEstateView;
        }

        public int CountRealState()
        {
            return _realEstateRepository.GetCount();
        }


        public override async Task Delete(int id)
        {
            await _realEstateImageService.RemoveAll(id);
            await _realEstateImprovementService.RemoveAll(id);
            await base.Delete(id);
        }
    }
}