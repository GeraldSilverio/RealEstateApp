using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Infraestructure.Identity.Entities;

namespace RealEstateApp.Infraestructure.Identity.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AdminService(UserManager<ApplicationUser> userManager, IAccountService accountService, IMapper mapper)
        {
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllAdmin()
        {
            var users = await _userManager.Users.ToListAsync();

            var adminUsers = users
                .Where(u => _userManager.GetRolesAsync(u).Result.Contains(Roles.Admin.ToString()))
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserName = u.UserName,
                    IdentityCard = u.IdentityCard,
                    ImageUser = u.ImageUser,
                    Roles = _userManager.GetRolesAsync(u).Result.ToList(),
                    IsActive = u.IsActive,
                })
                .ToList();

            return adminUsers;
        }

        public async Task<RegisterResponse> RegisterAdmin(SaveUserViewModel model)
        {
            model.SelectRole = (int)Roles.Admin;
            var request = _mapper.Map<RegisterRequest>(model);
            return await _accountService.RegisterAsync(request, null);
        }

        public int CountDevelopers(bool status)
        {
            var users = _userManager.Users.Where(u => u.IsActive == status).ToList();
            var devs = users.Where(u => _userManager.GetRolesAsync(u).Result.Contains(Roles.Developer.ToString())).Count();

            return devs;
        }
    }
}
