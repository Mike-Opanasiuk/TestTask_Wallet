using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Core.Entities;
using Wallet.Infrastructure.EntitiesConfiguration.Abstract;

namespace Wallet.Infrastructure.EntitiesConfiguration;

internal class TransactionEntityConfiguration 
    : BaseEntityConfiguration<TransactionEntity>
{
    public override void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(t => t.Type)
            .WithMany(tt => tt.Transactions)
            .HasForeignKey(t => t.TypeId);

        builder
            .HasOne(t => t.Category)
            .WithMany(tc => tc.Transactions)
            .HasForeignKey(t => t.CategoryId);

        builder
            .HasOne(t => t.Status)
            .WithMany(ts => ts.Transactions)
            .HasForeignKey(t => t.StatusId);

        builder
            .HasOne(t => t.AuthorizedUser)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.AuthorizedUserId)
            .IsRequired(false);
    }
}
