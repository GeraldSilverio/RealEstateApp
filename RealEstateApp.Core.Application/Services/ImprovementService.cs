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
        private readonly IImprovementRepository _improvementRepository;
        private readonly IRealEstateRepository _realEstateRepository;
        public ImprovementService(IImprovementRepository improvementRepository, IMapper mapper, 
            IRealEstateImprovementRepository realEstateImprovementRepository, IRealEstateService realEstateService, IRealEstateRepository realEstateRepository) : base(improvementRepository, mapper)
        {
            _realEstateImprovementRepository = realEstateImprovementRepository;
            _realEstateService = realEstateService;
            _mapper = mapper;
            _improvementRepository = improvementRepository;
            _realEstateRepository = realEstateRepository;

        }

        //No borrar quiero probar una cosa con el GetWithInclude ---ATT: Russ

        public async Task<List<ImprovementViewModel>> GetImprovementsNotInRealEstate(int idRealEstate)
        {
            //var realEstate = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "RealEstateImprovements.Improvement" });

            //foreach (var improvement in realEstate.Where(i => i.Id == idRealEstate))
            //{

            //}

            //var realEstate = await _realEstateService.GetRealEstateViewModelById(idRealEstate);

            //Lista para almacenar las mejoras que se encuentran en la propiedad seleccionada
            //var improvementsInRealEstate = new List<RealEstateImprovementViewModel>();
            //foreach (var improvementId in realEstate.ImprovementIds)
            //{
            //    var realEstateImprovements = _realEstateImprovementRepository.GetImprovementId(improvementId);
            //    var realEstateImprovementsViewModel = _mapper.Map<IEnumerable<RealEstateImprovementViewModel>>(realEstateImprovements);
            //    improvementsInRealEstate.AddRange(realEstateImprovementsViewModel);
            //}

            //var improvementIdsInRealEstate = improvementsInRealEstate.Select(x => x.IdImprovement);

            //var improvementsNotInRealEstate = new List<ImprovementViewModel>();


            //improvementsNotInRealEstate = improvements.Where(improvement =>
            //    !improvementIdsInRealEstate.Contains(improvement.Id))
            //    .ToList();

            //return improvementsNotInRealEstate;

            return null;
        }


        public async Task<List<ImprovementViewModel>> GetImprovementsInRealEstate(int idRealEstate, bool type)
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

  
            var listImprovements = new List<ImprovementViewModel>();

            if (!type)
            {
                //Obtener las mejoras que no se encuentran en la propiedad
                listImprovements = improvements.Where(improvement =>
                !improvementIdsInRealEstate.Contains(improvement.Id))
                .ToList();
            }
            else
            {
                //Obtener las mejoras en la propiedad
                listImprovements = improvements.Where(improvement =>
                improvementIdsInRealEstate.Contains(improvement.Id))
                .ToList();
            }           
            return listImprovements;
        }

        public async Task DeleteOneImprovement(int idImprovement, int idRealEstate)
        {
            await _realEstateImprovementRepository.RemoveOne(idImprovement, idRealEstate);
        }

    }
}
