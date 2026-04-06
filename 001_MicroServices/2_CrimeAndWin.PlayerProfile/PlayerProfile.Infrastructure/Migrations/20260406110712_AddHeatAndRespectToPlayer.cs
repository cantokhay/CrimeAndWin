using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHeatAndRespectToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HeatIndex",
                table: "Players",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RespectScore",
                table: "Players",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeatIndex",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RespectScore",
                table: "Players");
        }
    }
}
