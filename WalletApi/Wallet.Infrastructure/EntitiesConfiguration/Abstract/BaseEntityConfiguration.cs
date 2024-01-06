using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Infrastructure.EntitiesConfiguration.Abstract;

internal class BaseEntityConfiguration<TEntity>
    : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(u => u.CreatedOn).HasDefaultValueSql("now()").ValueGeneratedOnAdd();
        builder.Property(u => u.ModifiedOn).HasDefaultValueSql("now()").ValueGeneratedOnAddOrUpdate();
    }
}
