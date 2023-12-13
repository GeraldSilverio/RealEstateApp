using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IImprovementService:IGenericService<Improvement, SaveImprovementViewModel, ImprovementViewModel>
    {
        Task<List<ImprovementViewModel>> GetImprovementsInRealEstate(int idRealEstate, bool type);
        Task DeleteOneImprovement(int idImprovement, int idRealEstate);
    }
}
