using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IImprovementService:IGenericService<Improvement, SaveImprovementViewModel, ImprovementViewModel>
    {
        Task<List<ImprovementViewModel>> GetImprovementsNotInRealEstate(int idRealEstate);
    }
}
