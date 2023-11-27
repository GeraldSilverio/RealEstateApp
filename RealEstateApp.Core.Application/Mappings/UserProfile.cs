using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.ViewModels.User;
using static RealEstateApp.Core.Application.ViewModel.Login.ForgotPasswordViewModel;

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

            CreateMap<UpdateUserRequest, EditUserViewModel>()
              .ForMember(u => u.HasError, opt => opt.Ignore())
              .ForMember(u => u.Error, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<RegisterResponse, AuthenticationResponse>();

            CreateMap<AuthenticationResponse, UserViewModel>();

            CreateMap<AuthenticationResponse, UserStatusViewModel>()
                .ReverseMap();

            CreateMap<EditUserViewModel, AuthenticationResponse>()
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
