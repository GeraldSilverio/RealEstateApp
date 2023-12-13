using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.ViewModel.RealEstateImage;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IRealEstateImageService : IGenericService<RealEstateImage,SaveRealEstateImageViewModel,RealEstateImageViewModel>
    {
        string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "");
        Task<List<RealEstateImageViewModel>> GetAllByRealEstateId(int id);
        Task<List<string>> GetImagesByRealEstateId(int id);
        Task RemoveAll(int idRealEstate);
        Task DeleteOneImage(string image, int idRealEstate);
    }
}
