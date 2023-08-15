using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicinesManagement.Migrations
{
    /// <inheritdoc />
    public partial class leafletstorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leafletData",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "leafletUpdateDate",
                table: "Medicines");
        }
    }
}
