using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLILAB.Models;

namespace TestLILAB.DBContexts
{
    public class MySQLContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageObj> Images { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("products");
                e.HasKey(p => new { p.ID });

                e.HasOne(p => p.Image);
                e.HasOne(p => p.Category);

                e.HasData(new Product()
                {
                    ID = 1,
                    Name = "Manzana",
                    CategoryID = 1,
                    Description = "Estas son Manzanas",
                    Quantity = 10,
                    Price = 5.0,
                    ImageID = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                e.HasData(new Product()
                {
                    ID = 2,
                    Name = "Peras",
                    CategoryID = 1,
                    Description = "Estas son Peras",
                    Quantity = 30,
                    Price = 3.0,
                    ImageID = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                e.HasData(new Product()
                {
                    ID = 3,
                    Name = "Zanahorias",
                    CategoryID = 2,
                    Description = "Estas son Zanahorias",
                    Quantity = 30,
                    Price = 0.50,
                    ImageID = 3,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                e.HasData(new Product()
                {
                    ID = 4,
                    Name = "Papas",
                    CategoryID = 2,
                    Description = "Estas son Papas",
                    Quantity = 50,
                    Price = 0.8,
                    ImageID = 4,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("categories");
                e.HasKey(p => new { p.ID });

                e.HasData(new Category()
                {
                    ID = 1,
                    Name = "Frutas",
                    Description = "Esto son frutas",
                    UpdatedAt = DateTime.Now
                });

                e.HasData(new Category()
                {
                    ID = 2,
                    Name = "Verduras",
                    Description = "Esto son verduras",
                    UpdatedAt = DateTime.Now
                });
            });

            modelBuilder.Entity<ImageObj>(e =>
            {
                e.ToTable("images");
                e.HasKey(p => new { p.ID });

                e.HasData(new ImageObj()
                {
                    ID = 1,
                    Name = "Manzana",
                    Description = "Estas son Manzanas",
                    Source = "/images/products/manzanas.jpg",
                });

                e.HasData(new ImageObj()
                {
                    ID = 2,
                    Name = "Peras",
                    Description = "Estas son Peras",
                    Source = "/images/products/peras.jpg",
                });

                e.HasData(new ImageObj()
                {
                    ID = 3,
                    Name = "Zanahorias",
                    Description = "Estas son Zanahorias",
                    Source = "/images/products/zanahorias.jpg",
                });

                e.HasData(new ImageObj()
                {
                    ID = 4,
                    Name = "Papas",
                    Description = "Estas son Papas",
                    Source = "/images/products/papas.jpg",
                });
            });

            modelBuilder.Entity<Item>(e =>
            {
                e.ToTable("items");
                e.HasKey(p => new { p.ID });
                e.HasOne(e => e.Product);
                e.HasOne(e => e.Parent);
            });

            modelBuilder.Entity<ShopCart>(e =>
            {
                e.ToTable("shop_cart");
                e.HasKey(p => new { p.ID });
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("users");
                e.HasKey(p => new { p.ID });
            });
        }

    }
}
