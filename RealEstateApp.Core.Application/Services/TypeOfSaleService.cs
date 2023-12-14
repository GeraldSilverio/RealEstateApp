using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfSaleService : GenericService<TypeOfSale, SaveTypeOfSaleViewModel, TypeOfSaleViewModel>, ITypeOfSaleService
    {
        private readonly IRealEstateImageService _realEstateImageService;
        private readonly IRealEstateClientService _realEstateClientService;
        private readonly IRealEstateService _realEstateService;
        private readonly IAgentService _agentService;
        public TypeOfSaleService(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper, IRealEstateImageService realEstateImageService, IRealEstateClientService realEstateClientService, IRealEstateService realEstateService, IAgentService agentService) : base(typeOfSaleRepository, mapper)
        {
            _realEstateImageService = realEstateImageService;
            _realEstateClientService = realEstateClientService;
            _realEstateService = realEstateService;
            _agentService = agentService;
        }
        public override async Task Delete(int id)
        {
            var realEstateCount = await _realEstateService.GetRealEstateByTypeOfSaleAsync(id);
            foreach (var item in realEstateCount)
            {
                await _realEstateImageService.RemoveAll(item);
                await _realEstateClientService.RemoveAllByIdRealEstate(item);
            }
            //Actualizando el conteo de las propiedades del agente.
            var realEstates = await _realEstateService.GetAll();
            foreach (var realEstate in realEstates)
            {
                var count = await _realEstateService.GetAllByAgent(realEstate.IdAgent);
                await _agentService.IncrementRealEstate(realEstate.IdAgent, count.Count());
            }
            await base.Delete(id);
        }
    }
}
