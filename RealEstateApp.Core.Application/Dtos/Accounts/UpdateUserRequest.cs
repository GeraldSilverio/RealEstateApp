﻿namespace RealEstateApp.Core.Application.Dtos.Accounts
{
    public class UpdateUserRequest
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string IdentityCard { get; set; } = null!;
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
