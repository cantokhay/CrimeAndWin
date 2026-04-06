using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameWorld.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictsToGameWorld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_GameWorlds_GameWorldId",
                table: "Seasons");

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalRespectPoints = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    TaxRate = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, defaultValue: 0.05m),
                    MoneyBonusMultiplier = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 1m),
                    HeatDecayMultiplier = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 1m),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name",
                table: "Districts",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_GameWorlds_GameWorldId",
                table: "Seasons",
                column: "GameWorldId",
                principalTable: "GameWorlds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_GameWorlds_GameWorldId",
                table: "Seasons");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_GameWorlds_GameWorldId",
                table: "Seasons",
                column: "GameWorldId",
                principalTable: "GameWorlds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
