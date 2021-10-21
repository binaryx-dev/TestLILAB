﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestLILAB.DBContexts;

namespace TestLILAB.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20211021013140_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("TestLILAB.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.ToTable("categories");

                    b
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(2786),
                            Description = "Esto son frutas",
                            Name = "Frutas",
                            UpdatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(4096)
                        },
                        new
                        {
                            ID = 2,
                            CreatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(4768),
                            Description = "Esto son verduras",
                            Name = "Verduras",
                            UpdatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(4820)
                        });
                });

            modelBuilder.Entity("TestLILAB.Models.ImageObj", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.ToTable("images");

                    b
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "Estas son Manzanas",
                            Name = "Manzana",
                            Source = "/images/products/manzanas.jpg"
                        },
                        new
                        {
                            ID = 2,
                            Description = "Estas son Peras",
                            Name = "Peras",
                            Source = "/images/products/peras.jpg"
                        },
                        new
                        {
                            ID = 3,
                            Description = "Estas son Zanahorias",
                            Name = "Zanahorias",
                            Source = "/images/products/zanahorias.jpg"
                        },
                        new
                        {
                            ID = 4,
                            Description = "Estas son Papas",
                            Name = "Papas",
                            Source = "/images/products/papas.jpg"
                        });
                });

            modelBuilder.Entity("TestLILAB.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShopCartId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShopCartId");

                    b.ToTable("items");
                });

            modelBuilder.Entity("TestLILAB.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ImageID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ImageID");

                    b.ToTable("products");

                    b
                        .HasAnnotation("MySQL:Charset", "utf8");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CategoryID = 1,
                            CreatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 132, DateTimeKind.Local).AddTicks(3512),
                            Description = "Estas son Manzanas",
                            ImageID = 1,
                            Name = "Manzana",
                            Price = 5.0,
                            Quantity = 100,
                            UpdatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 132, DateTimeKind.Local).AddTicks(8872)
                        },
                        new
                        {
                            ID = 2,
                            CategoryID = 1,
                            CreatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2625),
                            Description = "Estas son Peras",
                            ImageID = 2,
                            Name = "Peras",
                            Price = 3.0,
                            Quantity = 30,
                            UpdatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2652)
                        },
                        new
                        {
                            ID = 3,
                            CategoryID = 2,
                            CreatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2782),
                            Description = "Estas son Zanahorias",
                            ImageID = 3,
                            Name = "Zanahorias",
                            Price = 0.5,
                            Quantity = 30,
                            UpdatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2784)
                        },
                        new
                        {
                            ID = 4,
                            CategoryID = 2,
                            CreatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2791),
                            Description = "Estas son Papas",
                            ImageID = 4,
                            Name = "Papas",
                            Price = 0.80000000000000004,
                            Quantity = 50,
                            UpdatedAt = new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2793)
                        });
                });

            modelBuilder.Entity("TestLILAB.Models.ShopCart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<double>("IGV")
                        .HasColumnType("double");

                    b.Property<double>("SubTotal")
                        .HasColumnType("double");

                    b.Property<double>("Total")
                        .HasColumnType("double");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("ID");

                    b.ToTable("shop_cart");
                });

            modelBuilder.Entity("TestLILAB.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ShopCartId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("TestLILAB.Models.Item", b =>
                {
                    b.HasOne("TestLILAB.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestLILAB.Models.ShopCart", "Parent")
                        .WithMany("Items")
                        .HasForeignKey("ShopCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TestLILAB.Models.Product", b =>
                {
                    b.HasOne("TestLILAB.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestLILAB.Models.ImageObj", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("TestLILAB.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TestLILAB.Models.ShopCart", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
