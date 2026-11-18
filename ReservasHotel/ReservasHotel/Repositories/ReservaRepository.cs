using Microsoft.EntityFrameworkCore;
using ReservasHotel.Data;
using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;

namespace ReservasHotel.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly ApplicationDbContext _context;

    public ReservaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reserva>> ObterTodasAsync() =>
        await _context.Reservas
            .Include(r => r.Hospede)
            .Include(r => r.Quarto)
            .OrderByDescending(r => r.DataEntrada)
            .ToListAsync();

    public async Task<Reserva?> ObterPorIdAsync(int id) =>
        await _context.Reservas
            .Include(r => r.Hospede)
            .Include(r => r.Quarto)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<bool> ExisteConflitoAsync(int quartoId, DateTime entrada, DateTime saida, int? ignorarReservaId = null) =>
        await _context.Reservas.AnyAsync(r =>
            r.QuartoId == quartoId
            && !r.Cancelada
            && (ignorarReservaId == null || r.Id != ignorarReservaId)
            && entrada < r.DataSaida
            && saida > r.DataEntrada);

    public async Task AdicionarAsync(Reserva reserva)
    {
        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Reserva reserva)
    {
        _context.Reservas.Update(reserva);
        await _context.SaveChangesAsync();
    }
}