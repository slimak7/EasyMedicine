using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class MedicinesTableChangeAddedPower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Power",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "Medicines");
        }
    }
}
