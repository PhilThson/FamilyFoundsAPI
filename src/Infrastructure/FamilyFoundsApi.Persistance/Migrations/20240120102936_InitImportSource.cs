using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFoundsApi.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitImportSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ImportSource",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { (short)1, null, "ING" },
                    { (short)2, null, "MILLENIUM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ImportSource",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "ImportSource",
                keyColumn: "Id",
                keyValue: (short)2);
        }
    }
}
