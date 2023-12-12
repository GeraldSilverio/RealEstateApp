using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.RealEstateImage;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Facade
{
    public class FacadeForAgent
    {
        private readonly IUserService _userService;
        private readonly IRealEstateService _realEstateService;
        private readonly IImprovementService _improvementService;
        private readonly ITypeOfRealEstateService _typeOfRealEstateService;
        private readonly ITypeOfSaleService _typeOfSaleService;
        private readonly IRealEstateImageService _realEstateImageService;
        private readonly IMapper _mapper;

        public FacadeForAgent(IUserService userService, IRealEstateService realEstateService,
            IImprovementService improvementService, ITypeOfRealEstateService typeOfRealEstateService, 
            ITypeOfSaleService typeOfSaleService, IRealEstateImageService realEstateImageService, IMapper mapper)
        {
            _userService = userService;
            _realEstateService = realEstateService;
            _improvementService = improvementService;
            _typeOfRealEstateService = typeOfRealEstateService;
            _typeOfSaleService = typeOfSaleService;
            _realEstateImageService = realEstateImageService;
            _mapper = mapper;
        }

        #region User
        public async Task<SaveUserViewModel> GetByAgentId(string idAgent) => await _userService.GetByUserIdAysnc(idAgent);
        public async Task UpdateAgent(SaveUserViewModel model) => await _userService.UpdateAsync(model);
        //public string UploadFileAgent(IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        //    => _userService.UploadFile(file, id, isEditMode, imagePath);
        #endregion

        #region RealEstate
        public async Task<List<RealEstateViewModel>> GetAllByAgentInSession(string idAgent)
            => await _realEstateService.GetAllByAgent(idAgent);
        public async Task<SaveRealEstateViewModel> GetRealEstateById(int id) => await _realEstateService.GetById(id);
        public async Task<SaveRealEstateViewModel> AddRealEstate(SaveRealEstateViewModel model) => await _realEstateService.Add(model);
        public async Task DeleteRealEstate(int id) => await _realEstateService.Delete(id);
        public async Task<SaveRealEstateViewModel> UpdateRealEstate(SaveRealEstateViewModel model, int id)
            => await _realEstateService.UpdateRealEstate(model, id);
        #endregion

        #region Improvement

        public async Task<List<ImprovementViewModel>> GetAllImprovements() => await _improvementService.GetAll();
        public async Task DeleteOneImprovementInRealEstate(int idImprovement, int idRealEstate) 
            => await _improvementService.DeleteOneImprovement(idImprovement, idRealEstate);
        public async Task<List<ImprovementViewModel>> GetImprovementsRealEstate(int id, bool type) 
            => await _improvementService.GetImprovementsInRealEstate(id, type);
        #endregion


        #region Type Of Sale
        public async Task<List<TypeOfSaleViewModel>> GetAllTypeOfSale() => await _typeOfSaleService.GetAll();
        #endregion


        #region Type of RealEstate
        public async Task<List<TypeOfRealStateViewModel>> GetAllTypeOfRealEstate() => await _typeOfRealEstateService.GetAll();
        #endregion

        #region RealEstate Image
        public async Task<List<RealEstateImageViewModel>> GetAllImageByRealEstate(int idRealEstate) 
            => await _realEstateImageService.GetAllByRealEstateId(idRealEstate);
        public async Task DeleteOneImageInRealEstate(string image, int idRealEstate) 
            => await _realEstateImageService.DeleteOneImage(image, idRealEstate);
        #endregion

        #region Mappings
        public async Task<RealEstateViewModel> GetByIdRealEstateMap(int id)
            => _mapper.Map<RealEstateViewModel>(await _realEstateService.GetById(id));
        #endregion
    }
}
