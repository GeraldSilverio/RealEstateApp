﻿using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAgentService
    {
        Task<List<UserViewModel>> GetAllAgentAsync();
        Task<UserViewModel> GetAgentByIdAsync(string id);
        Task<AuthenticationResponse> ChangeStatus(string id,bool status);
        Task<List<UserViewModel>> GetAllWithFilterAsync(string name);
        Task IncrementRealEstate(string idAgent,int count);
    }
}
