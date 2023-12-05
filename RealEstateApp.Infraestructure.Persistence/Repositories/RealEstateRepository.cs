using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateRepository : GenericRepository<RealEstate>, IRealEstateRepository
    {
        public RealEstateRepository(ApplicationContext dbContext) : base(dbContext)
        {            
        }
    }
}
