using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parcial_113917_Coniglio_Gabriel.Models;

public partial class Prog3AerolineasContext : DbContext
{
    public Prog3AerolineasContext()
    {
    }

    public Prog3AerolineasContext(DbContextOptions<Prog3AerolineasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avione> Aviones { get; set; }

    public virtual DbSet<Fabricante> Fabricantes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=LocalHost;Database=prog3_aerolineas;Port=5432;User Id=parcial;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Aviones_pk");

            entity.HasIndex(e => e.IdFabricante, "fki_fabricante");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdFabricanteNavigation).WithMany(p => p.Aviones)
                .HasForeignKey(d => d.IdFabricante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fabricante");
        });

        modelBuilder.Entity<Fabricante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Fabricantes_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Usuarios_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
