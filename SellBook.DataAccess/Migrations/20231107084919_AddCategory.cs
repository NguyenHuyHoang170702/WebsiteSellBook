using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
    public partial class AddCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_ID", "Category_Name", "CreatedDateTime", "DisplayOrder" },
                values: new object[] { 1, "Comestic", new DateTime(2023, 11, 7, 15, 49, 18, 919, DateTimeKind.Local).AddTicks(8212), 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_ID", "Category_Name", "CreatedDateTime", "DisplayOrder" },
                values: new object[] { 2, "Family", new DateTime(2023, 11, 7, 15, 49, 18, 919, DateTimeKind.Local).AddTicks(8223), 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_ID", "Category_Name", "CreatedDateTime", "DisplayOrder" },
                values: new object[] { 3, "History", new DateTime(2023, 11, 7, 15, 49, 18, 919, DateTimeKind.Local).AddTicks(8224), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Author", "ISBN", "ListPrice", "Price", "Price100", "Price50", "ProductDescription", "Title" },
                values: new object[] { 1, "LOL", "QWE", 10.0, 10.0, 6.0, 8.0, "1TTTTT", "TEST1" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Author", "ISBN", "ListPrice", "Price", "Price100", "Price50", "ProductDescription", "Title" },
                values: new object[] { 2, "LOL", "QWE", 10.0, 10.0, 6.0, 8.0, "2TTTTT", "TEST2" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Author", "ISBN", "ListPrice", "Price", "Price100", "Price50", "ProductDescription", "Title" },
                values: new object[] { 3, "LOL", "QWE", 10.0, 10.0, 6.0, 8.0, "3TTTTT", "TEST3" });
        }
    }
}
