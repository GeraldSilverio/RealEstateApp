using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstateClient;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.Helpers;

namespace RealEstateApp.Core.Application.Services
{
    public class RealEstateClientService : GenericService<RealEstateClient, SaveRealEstateClientViewModel, RealEstateClientViewModel>, IRealEstateClientService
    {
        private readonly IRealEstateClientRepository _realEstateClientRepository;
        private readonly IMapper _mapper;
        public RealEstateClientService(IRealEstateClientRepository realEstateClientRepository, IMapper mapper) : base(realEstateClientRepository, mapper)
        {
            _realEstateClientRepository = realEstateClientRepository;
            _mapper = mapper;
        }

        public async Task<List<RealEstateClientViewModel>> GetFavoritesByUserId(string idUser)
        {
            var favorites = _mapper.Map<List<RealEstateClientViewModel>>(await _realEstateClientRepository.GetAllByIdUser(idUser));
            return favorites;
        }
    }
}
