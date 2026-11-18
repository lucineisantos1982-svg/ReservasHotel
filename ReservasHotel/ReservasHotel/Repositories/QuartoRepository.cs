using Microsoft.EntityFrameworkCore;
using ReservasHotel.Data;
using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;

namespace ReservasHotel.Repositories;

public class QuartoRepository : IQuartoRepository
{
    private readonly ApplicationDbContext _context;

    public QuartoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Quarto>> ObterTodosAsync() =>
        await _context.Quartos
            .Include(q => q.TipoQuarto)
            .Where(q => q.Ativo)
            .OrderBy(q => q.Numero)
            .ToListAsync();

    public async Task<Quarto?> ObterPorIdAsync(int id) =>
        await _context.Quartos.Include(q => q.TipoQuarto).FirstOrDefaultAsync(q => q.Id == id);

    public async Task<List<Quarto>> ObterDisponiveisAsync(DateTime entrada, DateTime saida) =>
        await _context.Quartos
            .Include(q => q.TipoQuarto)
            .Where(q => q.Ativo
                        && !q.Reservas.Any(r => !r.Cancelada
                                                && entrada < r.DataSaida
                                                && saida > r.DataEntrada))
            .OrderBy(q => q.Numero)
            .ToListAsync();

    public async Task AdicionarAsync(Quarto quarto)
    {
        _context.Quartos.Add(quarto);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Quarto quarto)
    {
        _context.Quartos.Update(quarto);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(int id)
    {
        var quarto = await _context.Quartos.FindAsync(id);
        if (quarto != null)
        {
            quarto.Ativo = false;
            await _context.SaveChangesAsync();
        }
    }
}