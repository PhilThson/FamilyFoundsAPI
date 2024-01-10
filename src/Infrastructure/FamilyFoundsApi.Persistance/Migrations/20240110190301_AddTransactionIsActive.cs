using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFoundsApi.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Transaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Transaction");
        }
    }
}
