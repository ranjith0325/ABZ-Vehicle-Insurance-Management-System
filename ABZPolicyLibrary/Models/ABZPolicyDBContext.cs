using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ABZPolicyLibrary.Models;

public partial class ABZPolicyDBContext : DbContext
{
    public ABZPolicyDBContext()
    {
    }

    public ABZPolicyDBContext(DbContextOptions<ABZPolicyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<PolicyAddon> PolicyAddons { get; set; }

    public virtual DbSet<Proposal> Proposals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
     //   => optionsBuilder.UseSqlServer("data source=sqlsvrmani.database.windows.net; database=ABZPolicyDBProject; user id=mani; password=Root@1234");
     => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=ABZPolicyDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyNo).HasName("PK__Policy__2E1321971A5DD0A4");

            entity.ToTable("Policy");

            entity.Property(e => e.PolicyNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.NoClaimBonus).HasColumnType("money");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProposalNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiptNo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ProposalNoNavigation).WithMany(p => p.Policies)
                .HasForeignKey(d => d.ProposalNo)
                .HasConstraintName("FK__Policy__Proposal__286302EC");
        });

        modelBuilder.Entity<PolicyAddon>(entity =>
        {
            entity.HasKey(e => new { e.AddonID, e.PolicyNo }).HasName("PK__PolicyAd__16C9A70A513BFB88");

            entity.ToTable("PolicyAddon");

            entity.Property(e => e.AddonID)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PolicyNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Amount).HasColumnType("money");

            entity.HasOne(d => d.PolicyNoNavigation).WithMany(p => p.PolicyAddons)
                .HasForeignKey(d => d.PolicyNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PolicyAdd__Polic__29572725");
        });

        modelBuilder.Entity<Proposal>(entity =>
        {
            entity.HasKey(e => e.ProposalNo).HasName("PK__Proposal__6F39C9B36CAD1389");

            entity.ToTable("Proposal");

            entity.Property(e => e.ProposalNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
