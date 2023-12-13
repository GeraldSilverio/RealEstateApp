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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse? user;
        public RealEstateClientService(IRealEstateClientRepository realEstateClientRepository, IMapper mapper, IHttpContextAccessor contextAccessor) : base(realEstateClientRepository, mapper)
        {
            _realEstateClientRepository = realEstateClientRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<RealEstateClientViewModel>> GetFavoritesByUserId(string idUser)
        {
            var favorites = _mapper.Map<List<RealEstateClientViewModel>>(await _realEstateClientRepository.GetAllByIdUser(idUser));
            return favorites;
        }

        public override async Task Delete(int id)
        {
            var realEstate = await _realEstateClientRepository.GetByIdsAsync(id, user.Id);
            base.Delete(realEstate.Id);

            user.FavoritesRealEstate.Remove(realEstate.IdRealEstate);


        }
    }
}
