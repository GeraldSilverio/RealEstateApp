using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class ImprovementService : GenericService<Improvement, SaveImprovementViewModel, ImprovementViewModel>, IImprovementService
    {
        private readonly IRealEstateImprovementRepository _realEstateImprovementRepository;
        private readonly IRealEstateService _realEstateService;
        private readonly IMapper _mapper;
        public ImprovementService(IImprovementRepository improvementRepository, IMapper mapper, 
            IRealEstateImprovementRepository realEstateImprovementRepository, IRealEstateService realEstateService) : base(improvementRepository, mapper)
        {
            _realEstateImprovementRepository = realEstateImprovementRepository;
            _realEstateService = realEstateService;
            _mapper = mapper;
        }


        public async Task<List<ImprovementViewModel>> GetImprovementsNotInRealEstate(int idRealEstate)
        {   
            //Obtener todas las mejoras
            var improvements = await base.GetAll();

            //Obtener propiedad en epecifica 
            var realEstate = await _realEstateService.GetRealEstateViewModelById(idRealEstate);      

            //Lista para almacenar las mejoras que se encuentran en la propiedad seleccionada
            var improvementsInRealEstate = new List<RealEstateImprovementViewModel>();
            foreach (var improvementId in realEstate.ImprovementIds)
            {
                var realEstateImprovements = _realEstateImprovementRepository.GetImprovementId(improvementId);
                var realEstateImprovementsViewModel = _mapper.Map<IEnumerable<RealEstateImprovementViewModel>>(realEstateImprovements);
                improvementsInRealEstate.AddRange(realEstateImprovementsViewModel);
            }

            //Seleccionar los ids de las mejoras en la propiedad
            var improvementIdsInRealEstate = improvementsInRealEstate.Select(x => x.IdImprovement);
            
            //Obtener las mejoras que no se encuentran en la propiedad y almacenar en una lista
            var improvementsNotInRealEstate = new List<ImprovementViewModel>();           
            improvementsNotInRealEstate = improvements.Where(improvement =>
                !improvementIdsInRealEstate.Contains(improvement.Id))
                .ToList();

            return improvementsNotInRealEstate;
        }


        public async Task<List<ImprovementViewModel>> GetImprovementsInRealEstate(int idRealEstate)
        {
            //Obtener todas las mejoras
            var improvements = await base.GetAll();

            //Obtener propiedad en epecifica 
            var realEstate = await _realEstateService.GetRealEstateViewModelById(idRealEstate);

            //Lista para almacenar las mejoras que se encuentran en la propiedad seleccionada
            var improvementsInRealEstate = new List<RealEstateImprovements>();
            foreach (var improvementId in realEstate.ImprovementIds)
            {
                var realEstateImprovements = _realEstateImprovementRepository.GetImprovementId(improvementId);
                improvementsInRealEstate.AddRange(realEstateImprovements);
            }

            //Seleccionar los ids de las mejoras en la propiedad
            var improvementIdsInRealEstate = improvementsInRealEstate.Select(x => x.IdImprovement);

            //Obtener las mejoras que no se encuentran en la propiedad y almacenar en una lista
            var improvementsNotInRealEstate = new List<ImprovementViewModel>();
            improvementsNotInRealEstate = improvements.Where(improvement =>
                !improvementIdsInRealEstate.Contains(improvement.Id))
                .ToList();

            return improvementsNotInRealEstate;
        }

    }
}
