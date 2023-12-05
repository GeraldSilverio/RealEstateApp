using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstateImageRepository:IGenericRepository<RealEstateImage>
    {
        Task<List<RealEstateImage>> GetImagesByRealEstateId(int id);
    }
}
