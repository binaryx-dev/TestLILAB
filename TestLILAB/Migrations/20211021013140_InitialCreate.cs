using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TestLILAB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "shop_cart",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SubTotal = table.Column<double>(type: "double", nullable: false),
                    IGV = table.Column<double>(type: "double", nullable: false),
                    Total = table.Column<double>(type: "double", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_cart", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    ShopCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_products_categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ShopCartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_items_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_items_shop_cart_ShopCartId",
                        column: x => x.ShopCartId,
                        principalTable: "shop_cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "ID", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(2786), "Esto son frutas", "Frutas", new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(4096) },
                    { 2, new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(4768), "Esto son verduras", "Verduras", new DateTime(2021, 10, 20, 20, 31, 40, 135, DateTimeKind.Local).AddTicks(4820) }
                });

            migrationBuilder.InsertData(
                table: "images",
                columns: new[] { "ID", "Description", "Name", "Source", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Estas son Manzanas", "Manzana", "/images/products/manzanas.jpg", null },
                    { 2, "Estas son Peras", "Peras", "/images/products/peras.jpg", null },
                    { 3, "Estas son Zanahorias", "Zanahorias", "/images/products/zanahorias.jpg", null },
                    { 4, "Estas son Papas", "Papas", "/images/products/papas.jpg", null }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "ID", "CategoryID", "CreatedAt", "Description", "ImageID", "Name", "Price", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 10, 20, 20, 31, 40, 132, DateTimeKind.Local).AddTicks(3512), "Estas son Manzanas", 1, "Manzana", 5.0, 100, new DateTime(2021, 10, 20, 20, 31, 40, 132, DateTimeKind.Local).AddTicks(8872) },
                    { 2, 1, new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2625), "Estas son Peras", 2, "Peras", 3.0, 30, new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2652) },
                    { 3, 2, new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2782), "Estas son Zanahorias", 3, "Zanahorias", 0.5, 30, new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2784) },
                    { 4, 2, new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2791), "Estas son Papas", 4, "Papas", 0.80000000000000004, 50, new DateTime(2021, 10, 20, 20, 31, 40, 134, DateTimeKind.Local).AddTicks(2793) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_ProductId",
                table: "items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_items_ShopCartId",
                table: "items",
                column: "ShopCartId");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryID",
                table: "products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_products_ImageID",
                table: "products",
                column: "ImageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "shop_cart");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
