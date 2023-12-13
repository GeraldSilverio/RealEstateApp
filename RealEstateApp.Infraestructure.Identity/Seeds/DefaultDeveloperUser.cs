
using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infraestructure.Identity.Entities;

namespace RealEstateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultDeveloperUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "developeruser",
                Email = "developeruser@gmail.com",
                FirstName = "Usuario",
                LastName = "Developer",
                IdentityCard = "40865600000",
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
                    await userManager.CreateAsync(defaultUser, "123DeveloperC#");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Developer.ToString());
                }
            }

        }
    }
}
