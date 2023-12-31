﻿using Microsoft.EntityFrameworkCore;
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
            var realEstates = await Entities.Where(x => x.IdCliente == idUser).ToListAsync();
            return realEstates;
        }

        public async Task<RealEstateClient> GetByIdsAsync(int idRealEstate, string idUser)
        {
            var realEstate = await Entities.FirstOrDefaultAsync(x => x.IdCliente == idUser && x.IdRealEstate == idRealEstate);
            return realEstate;
        }

        public async Task RemoveAllByIdRealEstate(int idRealEstate)
        {
            var realEstateClients = await Entities.Where(x => x.IdRealEstate == idRealEstate).ToListAsync();

            foreach (var realEstateClient in realEstateClients)
            {
                Entities.Remove(realEstateClient);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
