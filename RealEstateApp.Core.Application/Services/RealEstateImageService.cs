using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstateImage;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateImageService : GenericService<RealEstateImage, SaveRealEstateImageViewModel, RealEstateImageViewModel>, IRealEstateImageService
    {
        private readonly IRealEstateImageRepository _realEstateImageRepository;
        private readonly IMapper _mapper;
        public RealEstateImageService(IRealEstateImageRepository realEstateImageRepository, IMapper mapper) : base(realEstateImageRepository, mapper)
        {
            _realEstateImageRepository = realEstateImageRepository;
            _mapper = mapper;
        }

        public async Task<List<string>> GetImagesByRealEstateId(int id)
        {
            var images = new List<string>();
            var realEstateImages = await _realEstateImageRepository.GetImagesByRealEstateId(id);
            foreach (var image in realEstateImages)
            {
                images.Add(image.Image);
            }
            return images;
        }

        public async Task<List<RealEstateImageViewModel>> GetAllByRealEstateId(int id)
        {
            var images = new List<RealEstateImageViewModel>();
            var realEstateImages = await _realEstateImageRepository.GetImagesByRealEstateId(id);

            foreach (var image in realEstateImages)
            {
                var imagesView = new RealEstateImageViewModel()
                {
                    Id = image.Id,
                    Image = image.Image,
                    IdRealEstate = image.IdRealEstate
                };

                images.Add(imagesView);
            }            

            return images;
        }

        public async Task RemoveAll(int idRealEstate)
        {
            await _realEstateImageRepository.RemoveAll(idRealEstate);
        }

        public async Task DeleteOneImage(string image, int idRealEstate)
        {
            await _realEstateImageRepository.RemoveOne(image, idRealEstate);
        }

        public string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string baseRoute = $"/Images/RealEstate/{id}";
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

    }
}
