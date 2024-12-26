﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ABZProductLibrary.Models;

public partial class ABZProductDBContext : DbContext
{
    public ABZProductDBContext()
    {
    }

    public ABZProductDBContext(DbContextOptions<ABZProductDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAddon> ProductAddons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       //=> optionsBuilder.UseSqlServer("data source=sqlsvrmani.database.windows.net; database=ABZProductDBProject; user id=mani; password=Root@1234");
       => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=ABZProductDB; integrated security=true");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductID).HasName("PK__Product__B40CC6EDADBB3215");

            entity.ToTable("Product");

            entity.Property(e => e.ProductID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.InsuredInterests)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PolicyCoverage)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ProductUIN)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<ProductAddon>(entity =>
        {
            entity.HasKey(e => new { e.ProductID, e.AddonID }).HasName("PK__ProductA__934E4FBC1CEB7B7F");

            entity.ToTable("ProductAddon");

            entity.Property(e => e.ProductID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AddonID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AddonDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AddonTitle)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAddons)
                .HasForeignKey(d => d.ProductID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductAd__Produ__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
