using AutoMapper;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.RealEstateImage;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Domain.Entities;
using System.Xml;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateService : GenericService<RealEstate, SaveRealEstateViewModel, RealEstateViewModel>, IRealEstateService
    {
        private readonly IRealEstateImageService _realEstateImageService;
        private readonly IRealEstateClientService _realEstateClientService;
        private readonly IRealEstateImprovementService _realEstateImprovementService;
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IUserService _userService;
        private readonly IAgentService _agentService;
        public RealEstateService(IRealEstateRepository realEstateRepository, IMapper mapper, IRealEstateImageService realEstateImageService, IRealEstateImprovementService realEstateImprovementService,
            IUserService userService, IRealEstateClientService realEstateClientService, IAgentService agentService) : base(realEstateRepository, mapper)
        {
            _realEstateImageService = realEstateImageService;
            _realEstateImprovementService = realEstateImprovementService;
            _realEstateRepository = realEstateRepository;
            _userService = userService;
            _realEstateClientService = realEstateClientService;
            _agentService = agentService;
        }

        #region Commons
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
            //Sumandole esa propiedad creada al agente.
            var count = await GetAllByAgent(realEstate.IdAgent);
            await _agentService.IncrementRealEstate(realEstate.IdAgent, count.Count());
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
            if (model.PropertiesImprovementsId.Count != 0)
            {
                foreach (var improvement in model.PropertiesImprovementsId)
                {
                    var realEstateImprovement = new SaveRealEstateImprovementViewModeL()
                    {
                        IdImprovement = improvement,
                        IdRealEstate = realEstate.Id
                    };
                    await _realEstateImprovementService.Add(realEstateImprovement);
                }
            }


            return realEstate;
        }

        public int CountRealState()
        {
            return _realEstateRepository.GetCount();
        }
        
        #endregion

        #region Updates
        public override async Task Update(SaveRealEstateViewModel model, int id)
        {
            await base.Update(model, id);
        }

        public async Task<SaveRealEstateViewModel> UpdateRealEstate(SaveRealEstateViewModel model, int id)
        {
            var code = await _realEstateRepository.GetByIdAsync(id);
            model.Code = code.Code;
            await base.Update(model, id);

            //Agregando nuevas imagenes
            if (model.Files is not null)
            {
                int imagesCount = model.Files.Count;
                var imagesExists = await _realEstateImageService.GetImagesByRealEstateId(id);
                int imagesTotal = imagesCount + imagesExists.Count;

                if (imagesTotal < 4)
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
            if (model.PropertiesImprovementsId is not null)
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

        #endregion

        #region Gets
        public override async Task<List<RealEstateViewModel>> GetAll()
        {
            var realEstateList = new List<RealEstateViewModel>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });

            foreach (var realEstate in realEstates)
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

        public async Task<List<RealEstateViewModel>> GetAllByAgent(string idUser)
        {
            var realEstateList = new List<RealEstateViewModel>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });

            foreach (var realEstate in realEstates.Where(x => x.IdAgent == idUser))
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

        public async Task<List<RealEstateViewModel>> GetFavoritesByClient()
        {
            var realEstateList = new List<RealEstateViewModel>();
            var realEstates = await GetAll();
            var favorites = await _realEstateClientService.GetFavoritesByUserId();

            foreach (var favorite in favorites)
            {
                var realEstate = realEstates.Find(x => x.Id == favorite.IdRealEstate);
                realEstateList.Add(realEstate);
            }
            return realEstateList;
        }

        public async Task<List<RealEstateViewModel>> GetAllWithFilters(string name, int? toilets, int? bedrooms, decimal? minPrice, decimal? maxPrice, string code)
        {
            var realEstates = await GetAll();

            return realEstates
                .Where(x => string.IsNullOrEmpty(name) || x.TypeOfRealEstateName == name)
                .Where(x => string.IsNullOrEmpty(code) || x.Code == code)
                .Where(x => !toilets.HasValue || x.BathRooms == toilets)
                .Where(x => !bedrooms.HasValue || x.BedRooms == bedrooms)
                .Where(x => !minPrice.HasValue || x.Price >= minPrice)
                .Where(x => !maxPrice.HasValue || x.Price <= maxPrice)
                .ToList();

        }

        public async Task<List<int>> GetRealEstateByTypeAsync(int IdTypeRealEstate)
        {
            return await _realEstateRepository.GetRealEstateByTypeAsync(IdTypeRealEstate);
        }

        public async Task<List<int>> GetRealEstateByTypeOfSaleAsync(int IdTypeOfSale)
        {
            return await _realEstateRepository.GetRealEstateByTypeOfSaleAsync(IdTypeOfSale);
        }
        #endregion

        #region Delete
        //Este metodo eliminara todos los agentes y sus propiedades, al igual que eliminara
        //todos los registros del cliente que haya marcado una de sus propiedades como favorito
        // y tambien las propiedades y sus imagenes.
        public async Task DeleteByAgent(string IdAgent)
        {
            var reaEstates = await GetAllByAgent(IdAgent);
            foreach (var item in reaEstates)
            {
                await _realEstateClientService.RemoveAllByIdRealEstate(item.Id);
                await _realEstateImageService.RemoveAll(item.Id);
            }
            await _realEstateRepository.DeleteByAgent(IdAgent);
        }
        public override async Task Delete(int id)
        {
            //Obteniendo la propiedad.
            var realEstate = await GetById(id);
            //Eliminando las imagenes, los favoritos y mejoras de esa propiedad
            await _realEstateImageService.RemoveAll(id);
            await _realEstateClientService.RemoveAllByIdRealEstate(id);
            await _realEstateImprovementService.RemoveAll(id);
            //Eliminando la propiedad
            await base.Delete(id);
            //Actualizando la cantidad de propiedades que tiene el usuario.
            var count = await GetAllByAgent(realEstate.IdAgent);
            await _agentService.IncrementRealEstate(realEstate.IdAgent, count.Count());
        }

        #endregion




    }
}