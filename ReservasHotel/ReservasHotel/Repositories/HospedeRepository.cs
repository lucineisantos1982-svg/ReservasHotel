using Microsoft.EntityFrameworkCore;
using ReservasHotel.Data;
using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;

namespace ReservasHotel.Repositories;

public class HospedeRepository : IHospedeRepository
{
    private readonly ApplicationDbContext _context;

    public HospedeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hospede>> ObterTodosAsync() =>
        await _context.Hospedes.Where(h => h.Ativo).OrderBy(h => h.Nome).ToListAsync();

    public async Task RemoverAsync(int id)
    {
        var hospede = await _context.Hospedes.FindAsync(id);
        if (hospede != null)
        {
            hospede.Ativo = false;
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Hospede?> ObterPorIdAsync(int id) =>
        await _context.Hospedes.FindAsync(id);

    public async Task AdicionarAsync(Hospede hospede)
    {
        _context.Hospedes.Add(hospede);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Hospede hospede)
    {
        _context.Hospedes.Update(hospede);
        await _context.SaveChangesAsync();
    }

}