using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyFoundsApi.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddImportSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Contractor",
                table: "Transaction",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "ImportSourceId",
                table: "Transaction",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ImportSource",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSource", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ImportSourceId",
                table: "Transaction",
                column: "ImportSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_ImportSource_ImportSourceId",
                table: "Transaction",
                column: "ImportSourceId",
                principalTable: "ImportSource",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_ImportSource_ImportSourceId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "ImportSource");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ImportSourceId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ImportSourceId",
                table: "Transaction");

            migrationBuilder.AlterColumn<string>(
                name: "Contractor",
                table: "Transaction",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);
        }
    }
}
