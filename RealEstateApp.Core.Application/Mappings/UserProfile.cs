using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;
using static RealEstateApp.Core.Application.ViewModel.Login.ForgotPassword;

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
                .ReverseMap();

            CreateMap<UpdateUserRequest, SaveUserViewModel>()
              .ForMember(u => u.HasError, opt => opt.Ignore())
              .ForMember(u => u.Error, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<RegisterResponse, AuthenticationResponse>();

            CreateMap<AuthenticationResponse, UserViewModel>();

            CreateMap<AuthenticationResponse, UserStatusViewModel>()
                .ReverseMap();

            CreateMap<SaveUserViewModel, AuthenticationResponse>()
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
