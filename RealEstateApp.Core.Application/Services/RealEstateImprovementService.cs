using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateImprovementService : GenericService<RealEstateImprovements, SaveRealEstateImprovementViewModeL, RealEstateImprovementViewModel>, IRealEstateImprovementService
    {
        public RealEstateImprovementService(IRealEstateImprovementRepository realEstateImprovementRepository, IMapper mapper) : base(realEstateImprovementRepository, mapper)
        {
        }
    }
}
