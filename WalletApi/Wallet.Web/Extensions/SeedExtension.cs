using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wallet.Core.Entities;
using Wallet.Infrastructure;
using Wallet.Shared;

namespace Wallet.Web.Extensions;


public static partial class WebApplicationExtensions
{
    public static void UseSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<RoleEntity>>();

            context.Database.Migrate();

            SeedRoles(roleManager).Wait();
        }
    }

    private static async Task SeedRoles(RoleManager<RoleEntity> roleManager)
    {
        var rolesToSeed = new string[] { AppConstant.Roles.User };

        foreach (var roleToSeed in rolesToSeed)
        {
            var role = new RoleEntity()
            {
                Name = roleToSeed
            };

            var isRoleExist = await roleManager.RoleExistsAsync(role.Name);

            if (!isRoleExist)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}