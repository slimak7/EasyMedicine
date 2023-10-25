using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class ATCaddfullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ATCFullCategory",
                table: "MedicineATCCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATCFullCategory",
                table: "MedicineATCCategories");
        }
    }
}
