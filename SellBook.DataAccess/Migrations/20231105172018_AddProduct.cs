using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
