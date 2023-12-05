using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IRealEstateImprovementService: IGenericService<RealEstateImprovements,SaveRealEstateImprovementViewModeL,RealEstateImprovementViewModel>
    {
    }
}
