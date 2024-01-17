using System;
using System.Collections.Generic;
using FullStackStappenplan.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStackStappenplan.Domains.Data;

public partial class BeerDbContext : DbContext
{
    public BeerDbContext()
    {
    }

    public BeerDbContext(DbContextOptions<BeerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brewery> Breweries { get; set; }

    public virtual DbSet<Variety> Varieties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL19_VIVES; Database=DB-Beer; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.Biernr).HasName("PK_Bieren");

            entity.Property(e => e.Alcohol).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Naam)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.BrouwernrNavigation).WithMany(p => p.Beers)
                .HasForeignKey(d => d.Brouwernr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bieren_Brouwerij");

            entity.HasOne(d => d.SoortnrNavigation).WithMany(p => p.Beers)
                .HasForeignKey(d => d.Soortnr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bieren_Soorten");
        });

        modelBuilder.Entity<Brewery>(entity =>
        {
            entity.HasKey(e => e.Brouwernr).HasName("PK_Brouwerij");

            entity.Property(e => e.Brouwernr).ValueGeneratedNever();
            entity.Property(e => e.Adres)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.Gemeente)
                .HasMaxLength(40)
                .IsFixedLength();
            entity.Property(e => e.Naam)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Omzet).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Postcode)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Variety>(entity =>
        {
            entity.HasKey(e => e.Soortnr).HasName("PK_Soorten");

            entity.Property(e => e.Soortnr).ValueGeneratedNever();
            entity.Property(e => e.Soortnaam)
                .HasMaxLength(30)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
