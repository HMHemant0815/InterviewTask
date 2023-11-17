using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PracticalTask1.Models;
using System.Data.Common;

namespace PracticalTask1
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ProductModel>(entity =>
            {

                entity.ToTable("Product");
                entity.HasKey(e => e.ProductId).HasName("PRIMARY");
                
                entity.Property(e => e.ProductId).HasColumnName("productId");
                entity.Property(e => e.Name).HasColumnName("productName");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.categoryId).HasColumnName("categoryId");

            });
            modelBuilder.Entity<CategoryModel>(entity =>
            {   

                entity.HasKey(e => e.categoryId).HasName("PRIMARY");
                entity.ToTable("Category");

                entity.Property(e => e.categoryId).HasColumnName("categoryId");
                entity.Property(e => e.categoryName).HasColumnName("categoryName");
            });


            modelBuilder.Entity<UserModel>(entity =>
            {

                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Fullname).HasMaxLength(200).IsRequired().HasColumnName("fullname");
                entity.Property(u => u.Username).HasMaxLength(100).IsRequired().HasColumnName("username");
                entity.Property(u => u.Password).HasMaxLength(100).IsRequired().HasColumnName("password");
                entity.Property(u => u.RoleType).HasMaxLength(20).IsRequired().HasColumnName("roletype");
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("getdate()").IsRequired().HasColumnName("createdAt");
                entity.Property(u => u.UpdatedAt).HasColumnName("updatedAt");
            });
           
        }

    }
}
