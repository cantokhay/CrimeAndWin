using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Economy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGangWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "WalletTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_WalletId",
                table: "WalletTransactions",
                newName: "IX_WalletTransactions_WalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletTransactions",
                table: "WalletTransactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GangWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlackBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CashBalance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MaxCapacity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 1000000m),
                    Note = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastTransactionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GangWallets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GangWallets_GangId",
                table: "GangWallets",
                column: "GangId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WalletTransactions_Wallets_WalletId",
                table: "WalletTransactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletTransactions_Wallets_WalletId",
                table: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "GangWallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletTransactions",
                table: "WalletTransactions");

            migrationBuilder.RenameTable(
                name: "WalletTransactions",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_WalletTransactions_WalletId",
                table: "Transactions",
                newName: "IX_Transactions_WalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
