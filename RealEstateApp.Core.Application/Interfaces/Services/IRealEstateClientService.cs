using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.RealEstateClient;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IRealEstateClientService : IGenericService<RealEstateClient, SaveRealEstateClientViewModel, RealEstateClientViewModel>
    {
        Task<List<RealEstateClientViewModel>> GetFavoritesByUserId();
        Task SetFavorites();
    }
}
