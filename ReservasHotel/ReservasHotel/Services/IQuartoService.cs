using ReservasHotel.Models;

namespace ReservasHotel.Services.Interfaces;

public interface IQuartoService
{
    Task<List<Quarto>> ObterTodosAsync();
    Task<Quarto?> ObterPorIdAsync(int id);
    Task<List<Quarto>> ObterDisponiveisAsync(DateTime entrada, DateTime saida);
    Task CriarAsync(Quarto quarto);
    Task AtualizarAsync(Quarto quarto);
    Task RemoverAsync(int id);
}