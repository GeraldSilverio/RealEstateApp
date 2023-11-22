using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "clientuser",
                Email = "clientuser@gmail.com",
                FirstName = "Usuario",
                LastName = "Cliente",
                IdentityCard = "40865662222",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123ClientC#");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
                }
            }

        }
    }
}
