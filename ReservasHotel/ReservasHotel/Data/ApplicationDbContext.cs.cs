using Microsoft.EntityFrameworkCore;
using ReservasHotel.Models;

namespace ReservasHotel.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hospede> Hospedes { get; set; }
    public DbSet<Quarto> Quartos { get; set; }
    public DbSet<TipoQuarto> TiposQuarto { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quarto>()
            .Property(q => q.Preco)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Hospede>()
            .Property(h => h.Ativo)
            .HasDefaultValue(true);

        modelBuilder.Entity<Quarto>()
            .Property(q => q.Ativo)
            .HasDefaultValue(true);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Quarto)
            .WithMany(q => q.Reservas)
            .HasForeignKey(r => r.QuartoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Hospede)
            .WithMany(h => h.Reservas)
            .HasForeignKey(r => r.HospedeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}