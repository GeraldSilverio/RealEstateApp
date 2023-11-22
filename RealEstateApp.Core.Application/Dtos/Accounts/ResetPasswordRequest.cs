
namespace RealEstateApp.Core.Application.Dtos.Accounts
{
    public class ResetPasswordRequest
    {
        public string UserName { get; set; }
        public string? Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
