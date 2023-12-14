using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class TypeOfRealEstateRepositoy : GenericRepository<TypeOfRealEstate>, ITypeOfRealEstateRepository
    {
        public TypeOfRealEstateRepositoy(ApplicationContext dbContext) : base(dbContext)
        {
        }

       
    }
}
