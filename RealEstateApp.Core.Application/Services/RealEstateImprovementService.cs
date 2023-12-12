using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateImprovementService : GenericService<RealEstateImprovements, SaveRealEstateImprovementViewModeL, RealEstateImprovementViewModel>, IRealEstateImprovementService
    {
        private readonly IRealEstateImprovementRepository _realEstateImprovementRepository;
        public RealEstateImprovementService(IRealEstateImprovementRepository realEstateImprovementRepository, IMapper mapper) : base(realEstateImprovementRepository, mapper)
        {
            _realEstateImprovementRepository = realEstateImprovementRepository;
        }
        public async Task RemoveAll(int idRealEstate)
        {
            await _realEstateImprovementRepository.RemoveAll(idRealEstate);
        }


        public async Task<List<string>> GetAllIfNotExist()
        {
            var improvements = new HashSet<string>();
            var improvementsInRealEstate = await base.GetAll();

            //foreach (var improvement in improvementsInRealEstate)
            //{
            //    if (!improvements.Contains(improvement.Name))
            //    {
            //        improvements.Add(improvement.Name);
            //    }
            //}

            return improvements.ToList();
        }
    }
}
