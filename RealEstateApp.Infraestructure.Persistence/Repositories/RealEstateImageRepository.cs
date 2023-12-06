using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateImageRepository : GenericRepository<RealEstateImage>, IRealEstateImageRepository
    {
        private readonly ApplicationContext _dbcontext;
        public RealEstateImageRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<RealEstateImage>> GetImagesByRealEstateId(int id)
        {
            return await _dbcontext.RealEstateImages.Where(x => x.IdRealEstate == id).ToListAsync();
        }

        public async Task RemoveAll(int idRealEstate)
        {
           var realEstateImages = await _dbcontext.RealEstateImages.Where(x => x.IdRealEstate == idRealEstate).ToListAsync();

            foreach (var realEstateImage in realEstateImages)
            {
                _dbcontext.RealEstateImages.Remove(realEstateImage);
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
