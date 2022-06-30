using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MealPlanAPI.Model
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Meal> Meals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ConnectionStrings:MealPlan");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>(entity =>
            {
                entity.Property(e => e.MealId).HasColumnName("mealId");

                entity.Property(e => e.CreateTimestamp).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("imageUrl");

                entity.Property(e => e.Title)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateTimestamp).HasDefaultValueSql("(sysutcdatetime())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
