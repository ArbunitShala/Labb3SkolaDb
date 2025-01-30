using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb3SkolaDb.Models;

public partial class AlmersContext : DbContext
{
    public AlmersContext()
    {
    }

    public AlmersContext(DbContextOptions<AlmersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Betyg> Betygs { get; set; }

    public virtual DbSet<Elever> Elevers { get; set; }

    public virtual DbSet<Klasser> Klassers { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Roller> Rollers { get; set; }

    public virtual DbSet<Ämne> Ämnes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=almers;Username=postgres;Password=Qorbb98765;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Betyg>(entity =>
        {
            entity.HasKey(e => e.Betygid).HasName("betyg_pkey");

            entity.ToTable("betyg");

            entity.Property(e => e.Betygid).HasColumnName("betygid");
            entity.Property(e => e.Betyg1)
                .HasMaxLength(1)
                .HasColumnName("betyg");
            entity.Property(e => e.Datum)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("datum");
            entity.Property(e => e.Elevid).HasColumnName("elevid");
            entity.Property(e => e.Lärareid).HasColumnName("lärareid");

            entity.HasOne(d => d.Elev).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.Elevid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("betyg_elevid_fkey");

            entity.HasOne(d => d.Lärare).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.Lärareid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("betyg_lärareid_fkey");

            entity.HasOne(d => d.Ämne).WithMany(p => p.Betygs)
                .HasForeignKey(d => d.Ämneid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("betyg_Ämneid_fkey");
        });

        modelBuilder.Entity<Elever>(entity =>
        {
            entity.HasKey(e => e.Elevid).HasName("elever_pkey");

            entity.ToTable("elever");

            entity.HasIndex(e => e.Personnummer, "elever_personnummer_key").IsUnique();

            entity.Property(e => e.Elevid).HasColumnName("elevid");
            entity.Property(e => e.Efternamn)
                .HasMaxLength(50)
                .HasColumnName("efternamn");
            entity.Property(e => e.Förnamn)
                .HasMaxLength(50)
                .HasColumnName("förnamn");
            entity.Property(e => e.Klassid).HasColumnName("klassid");
            entity.Property(e => e.Personnummer)
                .HasMaxLength(50)
                .HasColumnName("personnummer");

            entity.HasOne(d => d.Klass).WithMany(p => p.Elevers)
                .HasForeignKey(d => d.Klassid)
                .HasConstraintName("elever_klassid_fkey");
        });

        modelBuilder.Entity<Klasser>(entity =>
        {
            entity.HasKey(e => e.Klassid).HasName("klasser_pkey");

            entity.ToTable("klasser");

            entity.Property(e => e.Klassid).HasColumnName("klassid");
            entity.Property(e => e.Klassnamn)
                .HasMaxLength(50)
                .HasColumnName("klassnamn");
            entity.Property(e => e.Mentorid).HasColumnName("mentorid");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Klassers)
                .HasForeignKey(d => d.Mentorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("klasser_mentorid_fkey");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.Personalid).HasName("personal_pkey");

            entity.ToTable("personal");

            entity.HasIndex(e => e.Personnummer, "personal_personnummer_key").IsUnique();

            entity.Property(e => e.Personalid).HasColumnName("personalid");
            entity.Property(e => e.Efternamn)
                .HasMaxLength(50)
                .HasColumnName("efternamn");
            entity.Property(e => e.Förnamn)
                .HasMaxLength(50)
                .HasColumnName("förnamn");
            entity.Property(e => e.Personnummer)
                .HasMaxLength(12)
                .HasColumnName("personnummer");
            entity.Property(e => e.Rollid).HasColumnName("rollid");

            entity.HasOne(d => d.Roll).WithMany(p => p.Personals)
                .HasForeignKey(d => d.Rollid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personal_rollid_fkey");
        });

        modelBuilder.Entity<Roller>(entity =>
        {
            entity.HasKey(e => e.Rollid).HasName("roller_pkey");

            entity.ToTable("roller");

            entity.HasIndex(e => e.Rollnamn, "roller_rollnamn_key").IsUnique();

            entity.Property(e => e.Rollid).HasColumnName("rollid");
            entity.Property(e => e.Rollnamn)
                .HasMaxLength(50)
                .HasColumnName("rollnamn");
        });

        modelBuilder.Entity<Ämne>(entity =>
        {
            entity.HasKey(e => e.Ämneid).HasName("Ämne_pkey");

            entity.ToTable("Ämne");

            entity.HasIndex(e => e.Ämnenamn, "Ämne_Ämnenamn_key").IsUnique();

            entity.Property(e => e.Ämnenamn).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
