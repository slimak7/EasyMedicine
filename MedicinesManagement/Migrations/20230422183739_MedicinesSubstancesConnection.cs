using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class MedicinesSubstancesConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstances_Medicines_MedicineID",
                table: "ActiveSubstances");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstances_MedicineID",
                table: "ActiveSubstances");

            migrationBuilder.DropColumn(
                name: "MedicineID",
                table: "ActiveSubstances");

            migrationBuilder.CreateTable(
                name: "MedicineActiveSubstances",
                columns: table => new
                {
                    ConnectionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveSubstanceSubstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineActiveSubstances", x => x.ConnectionID);
                    table.ForeignKey(
                        name: "FK_MedicineActiveSubstances_ActiveSubstances_ActiveSubstanceSubstanceID",
                        column: x => x.ActiveSubstanceSubstanceID,
                        principalTable: "ActiveSubstances",
                        principalColumn: "SubstanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineActiveSubstances_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicineActiveSubstances_ActiveSubstanceSubstanceID",
                table: "MedicineActiveSubstances",
                column: "ActiveSubstanceSubstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineActiveSubstances_MedicineID",
                table: "MedicineActiveSubstances",
                column: "MedicineID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicineActiveSubstances");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicineID",
                table: "ActiveSubstances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstances_MedicineID",
                table: "ActiveSubstances",
                column: "MedicineID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstances_Medicines_MedicineID",
                table: "ActiveSubstances",
                column: "MedicineID",
                principalTable: "Medicines",
                principalColumn: "MedicineID");
        }
    }
}
