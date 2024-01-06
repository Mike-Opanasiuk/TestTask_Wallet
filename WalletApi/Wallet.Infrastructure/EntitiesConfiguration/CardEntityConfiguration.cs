using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Core.Entities;
using Wallet.Infrastructure.EntitiesConfiguration.Abstract;

namespace Wallet.Infrastructure.EntitiesConfiguration;

internal class CardEntityConfiguration : BaseEntityConfiguration<CardEntity>
{
    public override void Configure(EntityTypeBuilder<CardEntity> builder)
    {
        base.Configure(builder);

        builder.HasOne(c => c.Owner).WithMany(o => o.Cards).HasForeignKey(c => c.OwnerId);
    }
}
