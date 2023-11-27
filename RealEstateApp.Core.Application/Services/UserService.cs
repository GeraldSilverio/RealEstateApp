using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.ViewModels.User;

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

        #region RegisterMethods
        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveViewModel, string origin)
        {
            var request = _mapper.Map<RegisterRequest>(saveViewModel);
            var response = await _accountService.RegisterUserAsync(request, origin);
            return response;
        }

        public async Task<RegisterResponse> RegisterAdminAsync(SaveUserViewModel saveViewModel, string origin)
        {
            var request = _mapper.Map<RegisterRequest>(saveViewModel);
            var response = await _accountService.RegisterAdminUserAsync(request, origin);
            return response;
        }
        #endregion

        #region UpdateMethods
        public async Task UpdateAdmin(SaveUserViewModel vm, string id)
        {
            var request = _mapper.Map<UpdateUserRequest>(vm);
            await _accountService.UpdateAdminAsync(request, id);
        }

        public async Task UpdateUser(SaveUserViewModel vm, string id)
        {
            var request = _mapper.Map<UpdateUserRequest>(vm);
            await _accountService.UpdateUserAsync(request, id);
        }

        public async Task UpdateStatus(string id, bool status)
        {
            await _accountService.UpdateStatusAsync(id, status);
        }

        #endregion

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

        #region AllGets
        public List<UserViewModel> GetAllAsync()
        {
            var request = _accountService.GetAllUsersAsync();
            var user = _mapper.Map<List<UserViewModel>>(request);
            return user;
        }

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
        #endregion


    }
}
