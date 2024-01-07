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
            .HasOne(t => t.TransactionType)
            .WithMany(tt => tt.Transactions)
            .HasForeignKey(t => t.TransactionTypeId);

        builder
            .HasOne(t => t.TransactionCategory)
            .WithMany(tc => tc.Transactions)
            .HasForeignKey(t => t.TransactionCategoryId);

        builder
            .HasOne(t => t.TransactionStatus)
            .WithMany(ts => ts.Transactions)
            .HasForeignKey(t => t.TransactionStatusId);

        builder
            .HasOne(t => t.TransactionStatus)
            .WithMany(ts => ts.Transactions)
            .HasForeignKey(t => t.TransactionStatusId);

        builder
            .HasOne(t => t.AuthorizedUser)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.AuthorizedUserId)
            .IsRequired(false);
    }
}
