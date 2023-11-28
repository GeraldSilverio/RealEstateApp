using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        

        #region PasswordMethods
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewModel)
        {
            ResetPasswordRequest forgotRequest = _mapper.Map<ResetPasswordRequest>(viewModel);
            return await _accountService.ResetPasswordAsync(forgotRequest);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }
        #endregion


        public async Task<UserStatusViewModel> GetUserById(string id)
        {
            var request = await _accountService.GetUserByIdAsync(id);
            var user = _mapper.Map<UserStatusViewModel>(request);
            return user;
        }

        public async Task<SaveUserViewModel> GetUserViewModelById(string id)
        {

            var request = await _accountService.GetUserByIdAsync(id);
            var user = _mapper.Map<SaveUserViewModel>(request);
            return user;
        }

        public Task<List<UserViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
