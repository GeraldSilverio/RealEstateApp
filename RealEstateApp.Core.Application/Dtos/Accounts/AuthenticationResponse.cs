using RealEstateApp.Core.Application.ViewModel.RealEstateClient;
using System.Text.Json.Serialization;


namespace RealEstateApp.Core.Application.Dtos.Accounts
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string ImageUser { get; set; }
        public string IdentityCard { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string? JWToken { get; set; }
        public List<int>? FavoritesRealEstate { get; set; } = new();
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public string? Rol { get; set; } = null!;
        public int CountRealEstate { get; set; }
    }
}
