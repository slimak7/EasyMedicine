using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class ATCcategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ATCCategories",
                columns: table => new
                {
                    ATCCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ATCCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATCCategories", x => x.ATCCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "MedicineATCCategories",
                columns: table => new
                {
                    ConnectionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ATCCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineATCCategories", x => x.ConnectionID);
                    table.ForeignKey(
                        name: "FK_MedicineATCCategories_ATCCategories_ATCCategoryID",
                        column: x => x.ATCCategoryID,
                        principalTable: "ATCCategories",
                        principalColumn: "ATCCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineATCCategories_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineATCCategories_ATCCategoryID",
                table: "MedicineATCCategories",
                column: "ATCCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineATCCategories_MedicineID",
                table: "MedicineATCCategories",
                column: "MedicineID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineATCCategories");

            migrationBuilder.DropTable(
                name: "ATCCategories");
        }
    }
}
