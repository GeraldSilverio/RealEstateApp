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

        public Task<List<UserViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        //Debes enviarle un rol para el buscarte todos los usuarios con ese rol.
        public async Task<List<UserViewModel>> GetAllAsync(string entity)
        {
            var users = _mapper.Map<List<UserViewModel>>(await _accountService.GetAllAsync(entity));
            return users;
        }

        public async Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel model)
        {
            var request = _mapper.Map<RegisterRequest>(model);
            var response = await _accountService.RegisterAsync(request,null);
            var viewModel = _mapper.Map<SaveUserViewModel>(response);
            return viewModel;
        }

        public Task<SaveUserViewModel> GetByUserIdAysnc(string id)
        {
            throw new NotImplementedException();
        }
    }
}
