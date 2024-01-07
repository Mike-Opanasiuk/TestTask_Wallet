using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wallet.Core.Entities;
using Wallet.Infrastructure;
using Wallet.Shared.Services;
using static Wallet.Shared.AppConstant;

namespace Wallet.Web.Extensions;


public static partial class WebApplicationExtensions
{
    public static async void UseSeed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<RoleEntity>>();
            var imageService = scope.ServiceProvider.GetService<IImageService>();

            await context!.Database.MigrateAsync();

            await SeedRoles(roleManager!);
            await SeedTransactionTypesAsync(context!);
            await SeedTransactionStatusesAsync(context!);
            await SeedTransactionCategoriesAsync(context!, imageService!);
        }
    }

    private static async Task SeedRoles(RoleManager<RoleEntity> roleManager)
    {
        var rolesToSeed = new string[] { Roles.User };

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

    private static async Task SeedTransactionTypesAsync(AppDbContext context)
    {
        var transactionTypesToSeed = new KeyValuePair<Guid, string>[]
         {
            TransactionTypes.Payment,
            TransactionTypes.Credit
         };

        // so that seed will work only the first time
        if (context.TransactionTypes.Any())
        {
            return;
        }

        foreach (var type in transactionTypesToSeed)
        {
            await context.TransactionTypes.AddAsync(new TransactionTypeEntity()
            {
                Id = type.Key,
                Name = type.Value
            });
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedTransactionStatusesAsync(AppDbContext context)
    {
        var transactionStatusesToSeed = new KeyValuePair<Guid, string>[]
        {
            TransactionStatuses.Pending,
            TransactionStatuses.Approved,
            TransactionStatuses.Canceled
        };

        // so that seed will work only the first time
        if (context.TransactionStatuses.Any())
        {
            return;
        }

        foreach (var status in transactionStatusesToSeed)
        {
            await context.TransactionStatuses.AddAsync(new TransactionStatusEntity()
            {
                Id = status.Key,
                Name = status.Value
            });
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedTransactionCategoriesAsync(AppDbContext context, IImageService imageService)
    {
        var transactionCategoriesToSeed = new KeyValuePair<Guid, string>[]
        {
            TransactionCategories.Apple,
            TransactionCategories.IKEA,
            TransactionCategories.Target,
            TransactionCategories.Other
        };

        // so that seed will work only the first time
        if (context.TransactionCategories.Any())
        {
            return;
        }

        foreach (var category in transactionCategoriesToSeed)
        {
            var iconUrl = await imageService.LoadImageAsync("icons/", Shared.AppConstant.Random.SmallImgUrl);
            var backgroundImageUrl = await imageService.LoadImageAsync("images/", Shared.AppConstant.Random.BigImgUrl);

            await context.TransactionCategories.AddAsync(new TransactionCategoryEntity()
            {
                Id = category.Key,
                Name = category.Value,
                Icon = iconUrl,
                BackgroundImage = backgroundImageUrl
            });
        }

        await context.SaveChangesAsync();
    }
}