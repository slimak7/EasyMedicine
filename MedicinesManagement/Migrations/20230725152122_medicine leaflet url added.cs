using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class medicineleafleturladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "leafletURL",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leafletURL",
                table: "Medicines");
        }
    }
}
