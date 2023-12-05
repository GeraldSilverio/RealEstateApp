using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public LoginService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveViewModel, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(saveViewModel);
            return await _accountService.RegisterAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string origin)
        {
            return await _accountService.ConfirmAccountAsync(userId, origin);
        }

        public Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel viewModel, string origin)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel viewModel)
        {
            AuthenticationRequest authenticationRequest = _mapper.Map<AuthenticationRequest>(viewModel);
            AuthenticationResponse authenticationResponse = await _accountService.AuthenticateWebAppAsync(authenticationRequest);

            //Validacion especifica para los agentes que no se encuntran activos
            if (authenticationResponse.IsActive != true && authenticationResponse.HasError != false)
            {
                authenticationResponse.HasError = true;
                authenticationResponse.Error =
                    $"¡La cuenta con el usuario '{authenticationResponse.UserName}' se encuentra inactiva, comuniquese con un administrador!";
            }

            return authenticationResponse;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();

        }
    }
}
