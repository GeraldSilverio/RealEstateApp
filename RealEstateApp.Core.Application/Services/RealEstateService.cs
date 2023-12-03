using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateService : GenericService<RealEstate, SaveRealEstateViewModel, RealEstateViewModel>, IRealEstateService
    {

        public RealEstateService(IRealEstateRepository realEstateRepository, IMapper mapper) : base(realEstateRepository, mapper)
        {
            
        }
    }
}