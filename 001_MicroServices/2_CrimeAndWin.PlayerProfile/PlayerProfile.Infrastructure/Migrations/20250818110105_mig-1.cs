using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AvatarKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastEnergyCalcUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnergyCurrent = table.Column<int>(type: "int", nullable: false),
                    EnergyMax = table.Column<int>(type: "int", nullable: false),
                    EnergyRegenPerMinute = table.Column<int>(type: "int", nullable: false),
                    RankPosition = table.Column<int>(type: "int", nullable: true),
                    RankPoints = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    Luck = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
