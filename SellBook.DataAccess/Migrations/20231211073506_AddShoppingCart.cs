﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
	/// <inheritdoc />
	public partial class AddShoppingCart : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ShoppingCarts",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
					ProductId = table.Column<int>(type: "int", nullable: false),
					Count = table.Column<int>(type: "int", nullable: false),
					ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
					table.ForeignKey(
						name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ShoppingCarts_Products_ProductId",
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
				value: new DateTime(2023, 12, 11, 14, 35, 5, 785, DateTimeKind.Local).AddTicks(1046));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 2,
				column: "CreatedDateTime",
				value: new DateTime(2023, 12, 11, 14, 35, 5, 785, DateTimeKind.Local).AddTicks(1062));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 3,
				column: "CreatedDateTime",
				value: new DateTime(2023, 12, 11, 14, 35, 5, 785, DateTimeKind.Local).AddTicks(1063));

			migrationBuilder.CreateIndex(
				name: "IX_ShoppingCarts_ApplicationUserId",
				table: "ShoppingCarts",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ShoppingCarts_ProductId",
				table: "ShoppingCarts",
				column: "ProductId");


		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ShoppingCarts");

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 1,
				column: "CreatedDateTime",
				value: new DateTime(2023, 12, 10, 22, 43, 55, 963, DateTimeKind.Local).AddTicks(4586));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 2,
				column: "CreatedDateTime",
				value: new DateTime(2023, 12, 10, 22, 43, 55, 963, DateTimeKind.Local).AddTicks(4601));

			migrationBuilder.UpdateData(
				table: "Categories",
				keyColumn: "Category_ID",
				keyValue: 3,
				column: "CreatedDateTime",
				value: new DateTime(2023, 12, 10, 22, 43, 55, 963, DateTimeKind.Local).AddTicks(4602));
		}
	}
}
