using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfRealEstateService : GenericService<TypeOfRealEstate, SaveTypeOfRealStateViewModel, TypeOfRealStateViewModel>, ITypeOfRealEstateService
    {
        private readonly IRealEstateImageService _realEstateImageService;
        private readonly IRealEstateClientService _realEstateClientService;
        private readonly IRealEstateService _realEstateService;
        private readonly IAgentService _agentService;

        public TypeOfRealEstateService(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper, IRealEstateImageService realEstateImageService, IRealEstateClientService realEstateClientService, IRealEstateService realEstateService, IUserService userService, IAgentService agentService) : base(typeOfRealEstateRepository, mapper)
        {
            _realEstateImageService = realEstateImageService;
            _realEstateClientService = realEstateClientService;
            _realEstateService = realEstateService;
            _agentService = agentService;
        }

        public override async Task Delete(int id)
        {
            
            var realEstateCount = await _realEstateService.GetRealEstateByTypeAsync(id);
            foreach (var item in realEstateCount)
            {
               
                await _realEstateImageService.RemoveAll(item);
                await _realEstateClientService.RemoveAllByIdRealEstate(item);
            }
            await base.Delete(id);

            //Actualizando el conteo de las propiedades del agente.
            var realEstates = await _realEstateService.GetAll();
            foreach (var realEstate in realEstates)
            {
                var count = await _realEstateService.GetAllByAgent(realEstate.IdAgent);
                await _agentService.IncrementRealEstate(realEstate.IdAgent, count.Count());
            }



        }
    }
}
