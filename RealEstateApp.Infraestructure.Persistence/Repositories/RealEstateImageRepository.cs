using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateImageRepository : GenericRepository<RealEstateImage>, IRealEstateImageRepository
    {
        public RealEstateImageRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<RealEstateImage>> GetImagesByRealEstateId(int id)
        {
            return await Entities.Where(x => x.IdRealEstate == id).ToListAsync();
        }
        
    }
}
