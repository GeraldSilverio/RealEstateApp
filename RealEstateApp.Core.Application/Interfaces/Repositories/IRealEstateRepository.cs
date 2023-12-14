using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IRealEstateRepository : IGenericRepository<RealEstate>
    {
        Task<List<int>> GetRealEstateByTypeAsync(int IdTypeRealEstate); 
        Task<List<int>> GetRealEstateByTypeOfSaleAsync(int IdTypeOfSale);
        Task DeleteByAgent(string IdAgent);
    }
}
