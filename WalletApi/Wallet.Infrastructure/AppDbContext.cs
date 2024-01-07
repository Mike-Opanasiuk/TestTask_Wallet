using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Wallet.Core.Entities;

namespace Wallet.Infrastructure;

public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<PointEntity> Points { get; set; }
    public DbSet<TransactionTypeEntity> TransactionTypes { get; set; }
    public DbSet<TransactionCategoryEntity> TransactionCategories { get; set; }
    public DbSet<TransactionStatusEntity> TransactionStatuses { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        this.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
