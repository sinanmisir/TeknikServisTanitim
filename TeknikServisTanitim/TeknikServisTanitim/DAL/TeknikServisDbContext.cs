using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeknikServisTanitim.DAL;

public partial class TeknikServisDbContext : DbContext
{
    public TeknikServisDbContext()
    {
    }

    public TeknikServisDbContext(DbContextOptions<TeknikServisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DemoRequest> DemoRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-C0TUPMF\\SQLEXPRESS02;Database=TeknikServisDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DemoRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DemoRequ__3214EC078F6C86B0");

            entity.Property(e => e.AdSoyad).HasMaxLength(100);
            entity.Property(e => e.Durum)
                .HasMaxLength(50)
                .HasDefaultValue("Yeni");
            entity.Property(e => e.Eposta).HasMaxLength(100);
            entity.Property(e => e.FirmaAdi).HasMaxLength(150);
            entity.Property(e => e.TalepMesaji).HasMaxLength(1000);
            entity.Property(e => e.Tarih)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Telefon).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
