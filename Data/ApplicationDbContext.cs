using System;
using CRUD.Models.Entities;
using FLIP_CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FLIP_CRUD.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    // =================
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PostEntity> Posts { get; set; }

    // =========================
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply default value for CreatedAt and UpdatedAt to all entities inheriting BaseEntity
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            // {
            //     modelBuilder.Entity(entityType.ClrType)
            //         .Property(nameof(BaseEntity.CreatedAt))
            //         .HasDefaultValueSql("CURRENT_TIMESTAMP");

            //     modelBuilder.Entity(entityType.ClrType)
            //         .Property(nameof(BaseEntity.UpdatedAt))
            //         .HasDefaultValueSql("CURRENT_TIMESTAMP");
            // }

            // ===================
             if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(BaseEntity.CreatedAt))
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(BaseEntity.UpdatedAt))
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
        }
        }
    }


    // public override int SaveChanges()
    // {
    //     var entries = ChangeTracker
    //         .Entries()
    //         .Where(e => e.Entity is BaseEntity &&
    //                     (e.State == EntityState.Added || e.State == EntityState.Modified));

    //     foreach (var entry in entries)
    //     {
    //         var entity = (BaseEntity)entry.Entity;

    //         if (entry.State == EntityState.Added)
    //         {
    //             entity.CreatedAt = DateTime.UtcNow;
    //         }

    //         entity.UpdatedAt = DateTime.UtcNow;
    //     }

    //     return base.SaveChanges();
    // }
}
