using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstateImprovementRepository : IGenericRepository<RealEstateImprovements>
    {
        IQueryable<RealEstateImprovements> GetQueryableId(int id);
    }
}
