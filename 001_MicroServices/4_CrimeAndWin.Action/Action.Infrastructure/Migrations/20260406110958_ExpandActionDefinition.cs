using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Action.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandActionDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requirements_DifficultyLevel",
                table: "ActionDefinitions",
                newName: "DifficultyLevel");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseSuccessRate",
                table: "ActionDefinitions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 100m);

            migrationBuilder.AddColumn<decimal>(
                name: "HeatImpact",
                table: "ActionDefinitions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RespectImpact",
                table: "ActionDefinitions",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ActionDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSuccessRate",
                table: "ActionDefinitions");

            migrationBuilder.DropColumn(
                name: "HeatImpact",
                table: "ActionDefinitions");

            migrationBuilder.DropColumn(
                name: "RespectImpact",
                table: "ActionDefinitions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ActionDefinitions");

            migrationBuilder.RenameColumn(
                name: "DifficultyLevel",
                table: "ActionDefinitions",
                newName: "Requirements_DifficultyLevel");
        }
    }
}
