using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiFutbol.Models;

public partial class DbfutbolContext : DbContext
{
    public DbfutbolContext()
    {
    }

    public DbfutbolContext(DbContextOptions<DbfutbolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Jugador> Jugador { get; set; }

    public virtual DbSet<Liga> Ligas { get; set; }

    public virtual DbSet<Posicion> Posicions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("PK__EQUIPO__D8052408E05126B0");

            entity.ToTable("EQUIPO");

            entity.HasIndex(e => e.NombreEquipo, "UQ__EQUIPO__D94BAF2E67A3E374").IsUnique();

            entity.Property(e => e.NombreEquipo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaisEquipo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLigaNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdLiga)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Liga");
        });

        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.IdJugador).HasName("PK__JUGADOR__99E3201663C391F3");

            entity.ToTable("JUGADOR");

            entity.HasIndex(e => e.NombreJugador, "IDX_NombreJugador");

            entity.HasIndex(e => new { e.NombreJugador, e.IdEquipo }, "UQ_Jugador").IsUnique();

            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombreJugador)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaisJugador)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEquipoNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipo");

            entity.HasOne(d => d.IdPosicionNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdPosicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posicion");
        });

        modelBuilder.Entity<Liga>(entity =>
        {
            entity.HasKey(e => e.IdLiga).HasName("PK__LIGA__31D8EE1051DDC93B");

            entity.ToTable("LIGA");

            entity.HasIndex(e => e.NombreLiga, "UQ__LIGA__A97613F59F613B11").IsUnique();

            entity.Property(e => e.NombreLiga)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaisLiga)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Posicion>(entity =>
        {
            entity.HasKey(e => e.IdPosicion).HasName("PK__POSICION__638A2F4CFE2AF093");

            entity.ToTable("POSICION");

            entity.HasIndex(e => e.NombrePosicion, "UQ__POSICION__3D1477A54D85136C").IsUnique();

            entity.Property(e => e.NombrePosicion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
