
using MauiSqlite.Mobile.Modelos;
using MauiSqlite.Mobile.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace MauiSqlite.Mobile.DataAccess
{
    public class EAfiliadoDbContext : DbContext
    {
        public DbSet<EAfiliado> EAfiliados { get; set; }
        public DbSet<EGestion> EGestiones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("eafiliados.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EAfiliado>(entity =>
            {
                entity.HasKey(col => col.IdAfiliado);
                entity.Property(col => col.IdAfiliado).IsRequired().ValueGeneratedOnAdd();

                entity.HasOne(a => a.EGestion)
                .WithMany(g => g.Afiliados)
                .HasForeignKey(a => a.EGestionId);
            });

            modelBuilder.Entity<EGestion>(entity =>
            {
                entity.HasKey(col => col.Idges);
                entity.Property(col => col.Idges).IsRequired(); // No autogenerado
            });
        }
    }
}
