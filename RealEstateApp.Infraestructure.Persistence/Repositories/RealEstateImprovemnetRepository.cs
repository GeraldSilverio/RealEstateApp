using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateImprovemnetRepository : GenericRepository<RealEstateImprovements>, IRealEstateImprovementRepository
    {
        public RealEstateImprovemnetRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RealEstateImprovements> GetImprovementId(int id) => Entities.Where(x => x.IdImprovement == id);
    }
}
