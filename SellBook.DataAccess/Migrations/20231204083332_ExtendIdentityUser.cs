using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StressAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 4, 15, 33, 31, 902, DateTimeKind.Local).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 4, 15, 33, 31, 902, DateTimeKind.Local).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 4, 15, 33, 31, 902, DateTimeKind.Local).AddTicks(5867));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StressAddress",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 1,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 1, 15, 56, 47, 93, DateTimeKind.Local).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 1, 15, 56, 47, 93, DateTimeKind.Local).AddTicks(1466));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Category_ID",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 12, 1, 15, 56, 47, 93, DateTimeKind.Local).AddTicks(1500));
        }
    }
}
