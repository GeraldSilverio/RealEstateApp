using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstateClientRepository : IGenericRepository<RealEstateClient>
    {
        Task<List<RealEstateClient>> GetAllByIdUser(string idUser);
        Task<RealEstateClient> GetByIdsAsync(int idRealEstate, string idUser);
    }
}
