using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGangSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GangId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GangRole",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalRespectScore = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    MaxMemberLimit = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gangs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gangs_Tag",
                table: "Gangs",
                column: "Tag",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gangs");

            migrationBuilder.DropColumn(
                name: "GangId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GangRole",
                table: "Players");
        }
    }
}
