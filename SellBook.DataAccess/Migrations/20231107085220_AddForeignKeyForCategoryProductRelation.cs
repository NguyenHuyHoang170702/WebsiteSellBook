using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
	public partial class AddForeignKeyForCategoryProductRelation : Migration
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
					Price100 = table.Column<double>(type: "float", nullable: false),
					CategoryId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.Product_Id);
					table.ForeignKey(
						name: "FK_Products_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Category_ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 1,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 7, 15, 52, 20, 44, DateTimeKind.Local).AddTicks(7639));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 2,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 7, 15, 52, 20, 44, DateTimeKind.Local).AddTicks(7650));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 3,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 7, 15, 52, 20, 44, DateTimeKind.Local).AddTicks(7651));

			migrationBuilder.InsertData(
				table: "Products",
				columns: new[] { "Product_Id", "Author", "CategoryId", "ISBN", "ListPrice", "Price", "Price100", "Price50", "ProductDescription", "Title" },
				values: new object[,]
				{
					{ 1, "LOL", 2, "QWE", 10.0, 10.0, 6.0, 8.0, "1TTTTT", "TEST1" },
					{ 2, "LOL", 1, "QWE", 10.0, 10.0, 6.0, 8.0, "2TTTTT", "TEST2" },
					{ 3, "LOL", 2, "QWE", 10.0, 10.0, 6.0, 8.0, "3TTTTT", "TEST3" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Products");

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 1,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 7, 15, 49, 18, 919, DateTimeKind.Local).AddTicks(8212));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 2,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 7, 15, 49, 18, 919, DateTimeKind.Local).AddTicks(8223));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 3,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 7, 15, 49, 18, 919, DateTimeKind.Local).AddTicks(8224));
		}
	}
}
