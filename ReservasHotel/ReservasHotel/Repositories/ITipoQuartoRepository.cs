using ReservasHotel.Models;

namespace ReservasHotel.Repositories.Interfaces;

public interface ITipoQuartoRepository
{
    Task<List<TipoQuarto>> ObterTodosAsync();
    Task<TipoQuarto?> ObterPorIdAsync(int id);
    Task AdicionarAsync(TipoQuarto tipo);
    Task AtualizarAsync(TipoQuarto tipo);
    Task RemoverAsync(int id);
}