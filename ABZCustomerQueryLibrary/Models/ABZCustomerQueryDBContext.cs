using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ABZCustomerQueryLibrary.Models;

public partial class ABZCustomerQueryDBContext : DbContext
{
    public ABZCustomerQueryDBContext()
    {
    }

    public ABZCustomerQueryDBContext(DbContextOptions<ABZCustomerQueryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerQuery> CustomerQueries { get; set; }

    public virtual DbSet<QueryResponse> QueryResponses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=ABZCustomerQueryDB; integrated security=true");
          => optionsBuilder.UseSqlServer("data source=sqlsvrakshitha.database.windows.net; database=ABZCustomerQueryDB; user id=akshitha; password=Root@1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.AgentID).HasName("PK__Agent__9AC3BFD157511804");

            entity.ToTable("Agent");

            entity.Property(e => e.AgentID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerID).HasName("PK__Customer__A4AE64B8808B7BD6");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<CustomerQuery>(entity =>
        {
            entity.HasKey(e => e.QueryID).HasName("PK__Customer__5967F7FB9FC23368");

            entity.ToTable("CustomerQuery");

            entity.Property(e => e.QueryID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CustomerID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.QueryDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerQueries)
                .HasForeignKey(d => d.CustomerID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerQ__Custo__30F848ED");
        });

        modelBuilder.Entity<QueryResponse>(entity =>
        {
            entity.HasKey(e => new { e.QueryID, e.SrNo }).HasName("PK__QueryRes__855DBAC1A6A050D3");

            entity.ToTable("QueryResponse");

            entity.Property(e => e.QueryID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SrNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AgentID)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Agent).WithMany(p => p.QueryResponses)
                .HasForeignKey(d => d.AgentID)
                .HasConstraintName("FK__QueryResp__Agent__2C3393D0");

            entity.HasOne(d => d.Query).WithMany(p => p.QueryResponses)
                .HasForeignKey(d => d.QueryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QueryResp__Query__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
