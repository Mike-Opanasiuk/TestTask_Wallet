using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Core.Entities;
using Wallet.Infrastructure.EntitiesConfiguration.Abstract;

namespace Wallet.Infrastructure.EntitiesConfiguration;

internal class PointEntityConfiguration : BaseEntityConfiguration<PointEntity>
{
    public override void Configure(EntityTypeBuilder<PointEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Amount).IsRequired();
    }
}
