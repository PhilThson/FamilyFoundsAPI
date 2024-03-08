using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFoundsApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNumberIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transaction_Number_Account",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Number",
                table: "Transaction",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transaction_Number",
                table: "Transaction");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Number_Account",
                table: "Transaction",
                columns: new[] { "Number", "Account" },
                unique: true);
        }
    }
}
