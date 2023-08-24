using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class leafletsstorageindifferenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leafletData",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "leafletUpdateDate",
                table: "Medicines");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicineDataDataID",
                table: "Medicines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicineData",
                columns: table => new
                {
                    DataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    leafletData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    leafletUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineData", x => x.DataID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicineDataDataID",
                table: "Medicines",
                column: "MedicineDataDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicineData_MedicineDataDataID",
                table: "Medicines",
                column: "MedicineDataDataID",
                principalTable: "MedicineData",
                principalColumn: "DataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicineData_MedicineDataDataID",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "MedicineData");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_MedicineDataDataID",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MedicineDataDataID",
                table: "Medicines");

            migrationBuilder.AddColumn<byte[]>(
                name: "leafletData",
                table: "Medicines",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "leafletUpdateDate",
                table: "Medicines",
                type: "datetime2",
                nullable: true);
        }
    }
}
