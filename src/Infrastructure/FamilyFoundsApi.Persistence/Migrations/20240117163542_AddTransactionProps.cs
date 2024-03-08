using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFoundsApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transaction",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "Transaction",
                type: "TEXT",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractorAccountNumber",
                table: "Transaction",
                type: "TEXT",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractorBankName",
                table: "Transaction",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Transaction",
                type: "TEXT",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Transaction",
                type: "TEXT",
                maxLength: 128,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ContractorAccountNumber",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ContractorBankName",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Transaction");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transaction",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
