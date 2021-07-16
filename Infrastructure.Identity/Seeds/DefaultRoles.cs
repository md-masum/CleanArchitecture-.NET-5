using System.Threading.Tasks;
using Domain.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            var isSuperAdminExist = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            if (isSuperAdminExist is null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            }
            var isAdminExist = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            if (isAdminExist is null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }
            var isModeratorExist = await roleManager.FindByNameAsync(Roles.Moderator.ToString());
            if (isModeratorExist is null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            }
            var isBasicExist = await roleManager.FindByNameAsync(Roles.Basic.ToString());
            if (isBasicExist is null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
            }
        }
    }
}
