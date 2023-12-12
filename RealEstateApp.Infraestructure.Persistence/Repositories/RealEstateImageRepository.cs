using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateImageRepository : GenericRepository<RealEstateImage>, IRealEstateImageRepository
    {
        private readonly ApplicationContext _dbContext;
        public RealEstateImageRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RealEstateImage>> GetImagesByRealEstateId(int id)
        {
            return await Entities.Where(x => x.IdRealEstate == id).ToListAsync();
        }

        public async Task RemoveAll(int idRealEstate)
        {
           var realEstateImages = await Entities.Where(x => x.IdRealEstate == idRealEstate).ToListAsync();

            foreach (var realEstateImage in realEstateImages)
            {
                Entities.Remove(realEstateImage);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveOne(string image, int idRealEstate)
        {
            var realEstateImage = Entities.Where(x => x.Image == image && x.IdRealEstate == idRealEstate).FirstOrDefault();

            Entities.Remove(realEstateImage);
            _dbContext.SaveChangesAsync();
        }
    }
}
