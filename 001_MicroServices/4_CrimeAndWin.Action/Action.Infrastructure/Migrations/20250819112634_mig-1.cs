using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Action.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinPower = table.Column<int>(type: "int", nullable: false),
                    EnergyCost = table.Column<int>(type: "int", nullable: false),
                    PowerGain = table.Column<int>(type: "int", nullable: false),
                    ItemDrop = table.Column<bool>(type: "bit", nullable: false),
                    MoneyGain = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerActionAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessRate = table.Column<double>(type: "float(5)", precision: 5, scale: 2, nullable: false),
                    OutcomeType = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerActionAttempts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionDefinitions_Code",
                table: "ActionDefinitions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerActionAttempts_PlayerId_ActionDefinitionId_AttemptedAtUtc",
                table: "PlayerActionAttempts",
                columns: new[] { "PlayerId", "ActionDefinitionId", "AttemptedAtUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionDefinitions");

            migrationBuilder.DropTable(
                name: "PlayerActionAttempts");
        }
    }
}
