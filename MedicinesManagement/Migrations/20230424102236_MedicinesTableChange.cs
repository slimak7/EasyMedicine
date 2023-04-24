using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class MedicinesTableChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineDescription",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "MedicineDescription",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
