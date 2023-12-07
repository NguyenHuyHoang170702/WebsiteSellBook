using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SellBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CpmpanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StressAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CpmpanyId);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 8, 0, 37, 10, 134, DateTimeKind.Local).AddTicks(4282));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 8, 0, 37, 10, 134, DateTimeKind.Local).AddTicks(4298));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 8, 0, 37, 10, 134, DateTimeKind.Local).AddTicks(4299));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CpmpanyId", "City", "CompanyName", "PhoneNumber", "PostalCode", "State", "StressAddress" },
                values: new object[,]
                {
                    { 1, "Hanoi", "Company1", "0123456789", "1234", "KKKK", "N/A" },
                    { 2, "HoChiMinh", "Company2", "0198756789", "3456", "YYYY", "N/A" },
                    { 3, "DaNang", "Company3", "0123456123", "1298", "BBBB", "N/A" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 6, 15, 9, 15, 525, DateTimeKind.Local).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 6, 15, 9, 15, 525, DateTimeKind.Local).AddTicks(99));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 6, 15, 9, 15, 525, DateTimeKind.Local).AddTicks(102));
        }
    }
}
