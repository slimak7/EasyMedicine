using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveSubstancesManagement.Migrations
{
    /// <inheritdoc />
    public partial class predefineddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InteractionsLevel",
                columns: new[] { "InteractionLevelID", "InteractionLevelName", "InteractionLevelDescription" },
                values: new object[,] { { Guid.NewGuid().ToString(), "Low", "Ask your doctor"},
                    { Guid.NewGuid(), "Medium", "Not recommended" },
                    { Guid.NewGuid(), "High", "Forbidden" } }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
