using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveSubstancesManagement.Migrations
{
    /// <inheritdoc />
    public partial class InteractionsLevels_name_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_InteractionsLevel_InteractionLevelID",
                table: "Interactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionsLevel",
                table: "InteractionsLevel");

            migrationBuilder.RenameTable(
                name: "InteractionsLevel",
                newName: "InteractionsLevels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionsLevels",
                table: "InteractionsLevels",
                column: "InteractionLevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_InteractionsLevels_InteractionLevelID",
                table: "Interactions",
                column: "InteractionLevelID",
                principalTable: "InteractionsLevels",
                principalColumn: "InteractionLevelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_InteractionsLevels_InteractionLevelID",
                table: "Interactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionsLevels",
                table: "InteractionsLevels");

            migrationBuilder.RenameTable(
                name: "InteractionsLevels",
                newName: "InteractionsLevel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionsLevel",
                table: "InteractionsLevel",
                column: "InteractionLevelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_InteractionsLevel_InteractionLevelID",
                table: "Interactions",
                column: "InteractionLevelID",
                principalTable: "InteractionsLevel",
                principalColumn: "InteractionLevelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
