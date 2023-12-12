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
           var realEstateImages = await _dbContext.RealEstateImages.Where(x => x.IdRealEstate == idRealEstate).ToListAsync();

            foreach (var realEstateImage in realEstateImages)
            {
                _dbContext.RealEstateImages.Remove(realEstateImage);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
