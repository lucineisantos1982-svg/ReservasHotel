using ReservasHotel.Models;

namespace ReservasHotel.Services.Interfaces;

public interface ITipoQuartoService
{
    Task<List<TipoQuarto>> ObterTodosAsync();
    Task<TipoQuarto?> ObterPorIdAsync(int id);
    Task CriarAsync(TipoQuarto tipo);
    Task AtualizarAsync(TipoQuarto tipo);
    Task RemoverAsync(int id);
}