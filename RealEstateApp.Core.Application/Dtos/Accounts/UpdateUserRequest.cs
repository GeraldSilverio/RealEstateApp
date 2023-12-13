using Microsoft.AspNetCore.Http;

namespace RealEstateApp.Core.Application.Dtos.Accounts
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImageUser { get; set; }
        public string Phone { get; set; }
        public IFormFile? File { get; set; }
        public string IdentityCard { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
