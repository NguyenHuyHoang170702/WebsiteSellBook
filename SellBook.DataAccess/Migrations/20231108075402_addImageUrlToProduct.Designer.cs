﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SellBook.DataAccess;

#nullable disable

namespace SellBook.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231108075402_addImageUrlToProduct")]
    partial class addImageUrlToProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SellBook.Models.Category", b =>
                {
                    b.Property<int>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_ID"), 1L, 1);

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.HasKey("Category_ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Category_ID = 1,
                            Category_Name = "Comestic",
                            CreatedDateTime = new DateTime(2023, 11, 8, 14, 54, 1, 677, DateTimeKind.Local).AddTicks(5090),
                            DisplayOrder = 1
                        },
                        new
                        {
                            Category_ID = 2,
                            Category_Name = "Family",
                            CreatedDateTime = new DateTime(2023, 11, 8, 14, 54, 1, 677, DateTimeKind.Local).AddTicks(5112),
                            DisplayOrder = 1
                        },
                        new
                        {
                            Category_ID = 3,
                            Category_Name = "History",
                            CreatedDateTime = new DateTime(2023, 11, 8, 14, 54, 1, 677, DateTimeKind.Local).AddTicks(5113),
                            DisplayOrder = 1
                        });
                });

            modelBuilder.Entity("SellBook.Models.Product", b =>
                {
                    b.Property<int>("Product_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_Id"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Product_Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Product_Id = 1,
                            Author = "LOL",
                            CategoryId = 2,
                            ISBN = "QWE",
                            ListPrice = 10.0,
                            Price = 10.0,
                            Price100 = 6.0,
                            Price50 = 8.0,
                            ProductDescription = "1TTTTT",
                            ProductImageUrl = "",
                            Title = "TEST1"
                        },
                        new
                        {
                            Product_Id = 2,
                            Author = "LOL",
                            CategoryId = 1,
                            ISBN = "QWE",
                            ListPrice = 10.0,
                            Price = 10.0,
                            Price100 = 6.0,
                            Price50 = 8.0,
                            ProductDescription = "2TTTTT",
                            ProductImageUrl = "",
                            Title = "TEST2"
                        },
                        new
                        {
                            Product_Id = 3,
                            Author = "LOL",
                            CategoryId = 2,
                            ISBN = "QWE",
                            ListPrice = 10.0,
                            Price = 10.0,
                            Price100 = 6.0,
                            Price50 = 8.0,
                            ProductDescription = "3TTTTT",
                            ProductImageUrl = "",
                            Title = "TEST3"
                        });
                });

            modelBuilder.Entity("SellBook.Models.Product", b =>
                {
                    b.HasOne("SellBook.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
