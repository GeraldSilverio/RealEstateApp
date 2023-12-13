

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infraestructure.Identity.Entities;

namespace RealEstateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultAgentUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "agentuser",
                Email = "agentuser@gmail.com",
                FirstName = "Usuario",
                LastName = "Agente",
                IdentityCard = "40625601111",
                ImageUser = "/Images/Register/1895ba83-1a31-4fac-9871-96a47429a7b7/2484754c-db93-4c66-bb19-495d74dc8254.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                CountOfRealEstate = 0
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123AgentC#");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Agent.ToString());
                }
            }

        }
    }
}
