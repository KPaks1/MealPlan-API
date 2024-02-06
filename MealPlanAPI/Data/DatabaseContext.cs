using System;
using System.Collections.Generic;
using MealPlanAPI.Data.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MealPlanAPI.Data;
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
    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;

    public virtual DbSet<MealIngredient> MealIngredients { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("ConnectionStrings:MealPlan");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Meal 
        modelBuilder.Entity<Meal>(entity =>
        {
            entity.Property(e => e.MealId)
                .HasColumnName("MealId");

            entity.Property(e => e.Title)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("imageUrl");

            entity.Property(e => e.CreateTimestamp)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.Property(e => e.UpdateTimestamp)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        // Ingredient 
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.Property(e => e.IngredientId)
                .HasColumnName("IngredientId");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name");

            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.Cost)
                .HasColumnName("Cost");

            entity.Property(e => e.IngredientTypeId)
                .HasColumnName("IngredientTypeId");

            entity.Property(e => e.CreateTimestamp)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.Property(e => e.UpdateTimestamp)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        // IngredientType 
        modelBuilder.Entity<IngredientType>(entity =>
        {
            entity.Property(e => e.IngredientTypeId)
                .HasColumnName("IngredientTypeId");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name");

            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.CreateTimestamp)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.Property(e => e.UpdateTimestamp)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        // MealIngredient 
        modelBuilder.Entity<MealIngredient>(entity =>
        {
            entity.Property(e => e.IngredientId)
                .HasColumnName("IngredientTypeId");

            entity.Property(e => e.MealId)
                .HasColumnName("MealId");
        });

        OnModelCreatingPartial(modelBuilder);


    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}