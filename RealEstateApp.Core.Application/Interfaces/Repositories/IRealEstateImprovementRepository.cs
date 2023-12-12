using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstateImprovementRepository : IGenericRepository<RealEstateImprovements>
    {
        Task RemoveAll(int idRealEstate);
        IEnumerable<RealEstateImprovements> GetImprovementId(int id);
    }
}
