using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyFoundsApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SqlServerInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportSource",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Account = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Contractor = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContractorAccountNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ContractorBankName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CategoryId = table.Column<short>(type: "smallint", nullable: true),
                    ImportSourceId = table.Column<short>(type: "smallint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transaction_ImportSource_ImportSourceId",
                        column: x => x.ImportSourceId,
                        principalTable: "ImportSource",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Artykuły spożywcze" },
                    { (short)2, "Artykuły chemiczne i higieniczne" },
                    { (short)3, "Ubrania" },
                    { (short)4, "Rozrywka" },
                    { (short)5, "Transport" },
                    { (short)6, "Sprzęt domowy i budowlany" },
                    { (short)7, "Zdrowie i uroda" },
                    { (short)8, "Rachunki" },
                    { (short)9, "Dzieci" },
                    { (short)10, "Inne" }
                });

            migrationBuilder.InsertData(
                table: "ImportSource",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { (short)1, null, "ING" },
                    { (short)2, null, "MILLENNIUM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryId",
                table: "Transaction",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ImportSourceId",
                table: "Transaction",
                column: "ImportSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Number",
                table: "Transaction",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ImportSource");
        }
    }
}
