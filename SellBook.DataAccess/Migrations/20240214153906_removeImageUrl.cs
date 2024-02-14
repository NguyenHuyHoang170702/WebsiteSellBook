using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImageUrl",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2024, 2, 14, 22, 38, 58, 473, DateTimeKind.Local).AddTicks(204));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2024, 2, 14, 22, 38, 58, 473, DateTimeKind.Local).AddTicks(249));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2024, 2, 14, 22, 38, 58, 473, DateTimeKind.Local).AddTicks(253));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 24, 22, 20, 14, 445, DateTimeKind.Local).AddTicks(4513));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 24, 22, 20, 14, 445, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 24, 22, 20, 14, 445, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 1,
                column: "ProductImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 2,
                column: "ProductImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 3,
                column: "ProductImageUrl",
                value: "");
        }
    }
}
