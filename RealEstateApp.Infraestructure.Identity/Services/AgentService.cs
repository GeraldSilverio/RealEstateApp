using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infraestructure.Identity.Entities;

namespace RealEstateApp.Infraestructure.Identity.Services
{
    public class AgentService : IAgentService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AgentService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> ChangeStatus(string id, bool status)
        {
            AuthenticationResponse response = new AuthenticationResponse()
            {
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                response.HasError = true;
                response.Error = "Usuario no existe";
                return response;
            }
            user.IsActive = status;
            await _userManager.UpdateAsync(user);

            return response;
        }


        public async Task<AuthenticationResponse> GetAgentByIdAsync(string id)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userResponse = new AuthenticationResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                IdentityCard = user.IdentityCard,
                ImageUser = user.ImageUser,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                IsVerified = user.EmailConfirmed,
                IsActive = user.IsActive,
            };

            return userResponse;
        }

        public async Task<List<AuthenticationResponse>> GetAllAgentAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var user = users
                .Where(u => _userManager.GetRolesAsync(u).Result.Contains(Roles.Agent.ToString()))
                .Select(u => new AuthenticationResponse
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IdentityCard = u.IdentityCard,
                    Phone = u.PhoneNumber,
                    ImageUser = u.ImageUser,
                    Roles = _userManager.GetRolesAsync(u).Result.ToList(),
                    IsVerified = u.EmailConfirmed,
                    IsActive = u.IsActive,
                })
                .ToList();

            return user;
        }


    }
}
