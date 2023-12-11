using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateImprovemnetRepository : GenericRepository<RealEstateImprovements>, IRealEstateImprovementRepository
    {
        private readonly ApplicationContext _dbContext;
        public RealEstateImprovemnetRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task RemoveAll(int idRealEstate)
        {
            var realEstateImprovements = await _dbContext.RealEstateImprovements.Where(x => x.IdRealEstate == idRealEstate).ToListAsync();

            foreach (var realEstateImprovement in realEstateImprovements)
            {
                _dbContext.RealEstateImprovements.Remove(realEstateImprovement);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IQueryable<RealEstateImprovements> GetQueryableId(int id) => Entities.Where(x => x.IdImprovement == id);
    }
}
