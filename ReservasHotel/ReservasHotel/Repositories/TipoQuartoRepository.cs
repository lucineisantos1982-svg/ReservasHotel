using Microsoft.EntityFrameworkCore;
using ReservasHotel.Data;
using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;

namespace ReservasHotel.Repositories;

public class TipoQuartoRepository : ITipoQuartoRepository
{
    private readonly ApplicationDbContext _context;

    public TipoQuartoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TipoQuarto>> ObterTodosAsync() =>
        await _context.TiposQuarto.OrderBy(t => t.Nome).ToListAsync();

    public async Task<TipoQuarto?> ObterPorIdAsync(int id) =>
        await _context.TiposQuarto.FindAsync(id);

    public async Task AdicionarAsync(TipoQuarto tipo)
    {
        _context.TiposQuarto.Add(tipo);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(TipoQuarto tipo)
    {
        _context.TiposQuarto.Update(tipo);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var tipo = await _context.TiposQuarto.FindAsync(id);
        if (tipo != null)
        {
            _context.TiposQuarto.Remove(tipo);
            await _context.SaveChangesAsync();
        }
    }
}