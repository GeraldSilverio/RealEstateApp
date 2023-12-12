using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infraestructure.Persistence.Context;

namespace RealEstateApp.Infraestructure.Persistence.Repositories
{
    public class RealEstateClientRepository : GenericRepository<RealEstateClient>, IRealEstateClientRepository
    {
        private readonly ApplicationContext _dbContext;
        public RealEstateClientRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RealEstateClient>> GetAllByIdUser(string idUser)
        {
            var realEstates = await _dbContext.RealEstateClients.Where(x => x.IdCliente == idUser).ToListAsync();
            return realEstates;
        }
    }
}
