using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region User Profile

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(u => u.HasError, opt => opt.Ignore())
                .ForMember(u => u.Error, opt => opt.Ignore())
                .ForMember(u => u.PhoneNumber, src => src.MapFrom(dest => dest.Phone))
                .ReverseMap();

            CreateMap<RegisterResponse, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<UpdateUserRequest, SaveUserViewModel>()
              .ForMember(u => u.HasError, opt => opt.Ignore())
              .ForMember(u => u.Error, opt => opt.Ignore())
              .ForMember(u => u.PhoneNumber, src => src.MapFrom(dest => dest.Phone))
              .ReverseMap();

            CreateMap<RegisterResponse, AuthenticationResponse>()
            .ForMember(u => u.FavoritesRealEstate, opt => opt.Ignore());

            CreateMap<AuthenticationResponse, UserViewModel>()
              .ForMember(u => u.PhoneNumber, src => src.MapFrom(dest => dest.Phone))
              .ReverseMap()
              .ForMember(u => u.FavoritesRealEstate, opt => opt.Ignore());

            CreateMap<AuthenticationResponse, UpdateUserRequest>()
                .ReverseMap()
            .ForMember(u => u.FavoritesRealEstate, opt => opt.Ignore());

            CreateMap<SaveUserViewModel, AuthenticationResponse>()
                .ForMember(u => u.FavoritesRealEstate, opt => opt.Ignore())
                .ReverseMap()
              .ForMember(u => u.PhoneNumber, src => src.MapFrom(dest => dest.Phone));


            CreateMap<SaveUserViewModel, MyProfileViewModel>()
                .ReverseMap();




            #endregion

            #region Login Profile

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(u => u.HasError, opt => opt.Ignore())
                .ForMember(u => u.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ReverseMap();

            #endregion
        }
    }
}
