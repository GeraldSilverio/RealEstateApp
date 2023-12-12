using AutoMapper;
using RealEstateApp.Core.Application.ViewModel.RealEstateClient;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mappings
{
    public class RealEstateClientProfile : Profile
    {
        public RealEstateClientProfile()
        {
            CreateMap<RealEstateClient, SaveRealEstateClientViewModel>()
                .ReverseMap();

            CreateMap<RealEstateClient, RealEstateClientViewModel>()
                .ReverseMap();
        }
    }
}
