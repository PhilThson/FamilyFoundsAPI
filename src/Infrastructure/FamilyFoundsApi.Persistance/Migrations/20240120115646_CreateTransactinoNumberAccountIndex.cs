using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFoundsApi.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CreateTransactinoNumberAccountIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Number_Account",
                table: "Transaction",
                columns: new[] { "Number", "Account" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transaction_Number_Account",
                table: "Transaction");
        }
    }
}
