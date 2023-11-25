using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class TypeOfSaleRepository : GenericRepository<TypeOfSale>,ITypeOfSaleRepository
    {
        public TypeOfSaleRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
