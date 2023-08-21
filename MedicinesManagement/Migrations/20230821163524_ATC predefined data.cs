using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class ATCpredefineddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ATCCategoryDescription",
                table: "ATCCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ATCCategories",
                columns: new[] { "ATCCategoryID", "ATCCategoryName", "ATCCategoryDescription" },
                values: new object[,] {
                    { Guid.NewGuid().ToString(), "A", "Alimentary tract and metabolism"},
                    { Guid.NewGuid().ToString(), "B", "Blood and blood forming organs"},
                    { Guid.NewGuid().ToString(), "C", "Cardiovascular system"},
                    { Guid.NewGuid().ToString(), "D", "Dermatologicals"},
                    { Guid.NewGuid().ToString(), "G", "Genito-urinary system and sex hormones"},
                    { Guid.NewGuid().ToString(), "H", "Systemic hormonal preparations, excluding sex hormones and insulins"},
                    { Guid.NewGuid().ToString(), "J", "Antiinfectives for systemic use"},
                    { Guid.NewGuid().ToString(), "L", "Antineoplastic and immunomodulating agents"},
                    { Guid.NewGuid().ToString(), "M", "Musculo-skeletal system"},
                    { Guid.NewGuid().ToString(), "N", "Nervous system"},
                    { Guid.NewGuid().ToString(), "P", "Antiparasitic products, insecticides and repellents"},
                    { Guid.NewGuid().ToString(), "R", "Respiratory system"},
                    { Guid.NewGuid().ToString(), "S", "Sensory organs"},
                    { Guid.NewGuid().ToString(), "V", "Various"}
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATCCategoryDescription",
                table: "ATCCategories");
        }
    }
}
