using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Action.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteActionMechanics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "PlayerEnergyStates",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "PlayerEnergyStates",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlayerEnergyStates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "PlayerEnergyStates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Requirements_DifficultyLevel",
                table: "ActionDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlayerEnergyStates");

            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                table: "PlayerEnergyStates");

            migrationBuilder.DropColumn(
                name: "Requirements_DifficultyLevel",
                table: "ActionDefinitions");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "PlayerEnergyStates",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlayerEnergyStates",
                newName: "PlayerId");
        }
    }
}


