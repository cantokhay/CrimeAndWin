using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Economy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletForDualEconomy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BlackBalance",
                table: "Wallets",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CashBalance",
                table: "Wallets",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BalanceType",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackBalance",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CashBalance",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "BalanceType",
                table: "Transactions");
        }
    }
}
