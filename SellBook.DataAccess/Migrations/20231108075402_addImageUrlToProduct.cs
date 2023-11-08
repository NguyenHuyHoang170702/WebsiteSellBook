using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
	public partial class addImageUrlToProduct : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "ProductImageUrl",
				table: "Products",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 1,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 8, 14, 54, 1, 677, DateTimeKind.Local).AddTicks(5090));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 2,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 8, 14, 54, 1, 677, DateTimeKind.Local).AddTicks(5112));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 3,
				column: "CreatedDateTime",
				value: new DateTime(2023, 11, 8, 14, 54, 1, 677, DateTimeKind.Local).AddTicks(5113));

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

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ProductImageUrl",
				table: "Products");

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
		}
	}
}
