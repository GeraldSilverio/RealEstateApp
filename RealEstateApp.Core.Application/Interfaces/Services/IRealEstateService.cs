using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IRealEstateService : IGenericService<RealEstate, SaveRealEstateViewModel, RealEstateViewModel>
    {
        Task<RealEstateViewModel> GetRealEstateViewModelById(int id);
        int CountRealState();
    }
}