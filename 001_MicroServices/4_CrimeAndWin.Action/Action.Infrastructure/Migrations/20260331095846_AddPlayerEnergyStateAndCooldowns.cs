using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Action.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerEnergyStateAndCooldowns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CooldownEndsAt",
                table: "PlayerActionAttempts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                table: "PlayerActionAttempts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "PlayerActionAttempts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PlayerActionAttempt_SuccessRate",
                table: "PlayerActionAttempts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "PlayerEnergyStates",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentEnergy = table.Column<int>(type: "int", nullable: false),
                    LastRefillAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveBoostItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoostExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerEnergyStates", x => x.PlayerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerEnergyStates");

            migrationBuilder.DropColumn(
                name: "CooldownEndsAt",
                table: "PlayerActionAttempts");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "PlayerActionAttempts");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "PlayerActionAttempts");

            migrationBuilder.DropColumn(
                name: "PlayerActionAttempt_SuccessRate",
                table: "PlayerActionAttempts");
        }
    }
}

