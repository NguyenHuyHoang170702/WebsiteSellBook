using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2024, 2, 14, 22, 48, 38, 805, DateTimeKind.Local).AddTicks(6727));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2024, 2, 14, 22, 48, 38, 805, DateTimeKind.Local).AddTicks(6768));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2024, 2, 14, 22, 48, 38, 805, DateTimeKind.Local).AddTicks(6770));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

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
    }
}
