using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardEntity_AspNetUsers_OwnerId",
                table: "CardEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PointEntity_CardEntity_CardId",
                table: "PointEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointEntity",
                table: "PointEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardEntity",
                table: "CardEntity");

            migrationBuilder.RenameTable(
                name: "PointEntity",
                newName: "Points");

            migrationBuilder.RenameTable(
                name: "CardEntity",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_PointEntity_CardId",
                table: "Points",
                newName: "IX_Points_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_CardEntity_OwnerId",
                table: "Cards",
                newName: "IX_Cards_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_AspNetUsers_OwnerId",
                table: "Cards",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Cards_CardId",
                table: "Points",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_OwnerId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Points_Cards_CardId",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "PointEntity");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "CardEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Points_CardId",
                table: "PointEntity",
                newName: "IX_PointEntity_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_OwnerId",
                table: "CardEntity",
                newName: "IX_CardEntity_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointEntity",
                table: "PointEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardEntity",
                table: "CardEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardEntity_AspNetUsers_OwnerId",
                table: "CardEntity",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PointEntity_CardEntity_CardId",
                table: "PointEntity",
                column: "CardId",
                principalTable: "CardEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
