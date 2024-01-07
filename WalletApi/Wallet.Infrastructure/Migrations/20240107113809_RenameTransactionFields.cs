using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTransactionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionStatuses_TransactionStatusId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                table: "Transactions",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "TransactionStatusId",
                table: "Transactions",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "TransactionCategoryId",
                table: "Transactions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                newName: "IX_Transactions_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionStatusId",
                table: "Transactions",
                newName: "IX_Transactions_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions",
                newName: "IX_Transactions_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionStatuses_StatusId",
                table: "Transactions",
                column: "StatusId",
                principalTable: "TransactionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TypeId",
                table: "Transactions",
                column: "TypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCategories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionStatuses_StatusId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TypeId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Transactions",
                newName: "TransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Transactions",
                newName: "TransactionStatusId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Transactions",
                newName: "TransactionCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                newName: "IX_Transactions_TransactionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_StatusId",
                table: "Transactions",
                newName: "IX_Transactions_TransactionStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                newName: "IX_Transactions_TransactionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionStatuses_TransactionStatusId",
                table: "Transactions",
                column: "TransactionStatusId",
                principalTable: "TransactionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
