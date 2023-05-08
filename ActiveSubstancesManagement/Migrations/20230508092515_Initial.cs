using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveSubstancesManagement.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InteractionsLevel",
                columns: table => new
                {
                    InteractionLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteractionLevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteractionLevelDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionsLevel", x => x.InteractionLevelID);
                });

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    InteractionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicineID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteractedSubstanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteractionLevelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.InteractionID);
                    table.ForeignKey(
                        name: "FK_Interactions_InteractionsLevel_InteractionLevelID",
                        column: x => x.InteractionLevelID,
                        principalTable: "InteractionsLevel",
                        principalColumn: "InteractionLevelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_InteractionLevelID",
                table: "Interactions",
                column: "InteractionLevelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "InteractionsLevel");
        }
    }
}
