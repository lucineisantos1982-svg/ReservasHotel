using ReservasHotel.Models;

namespace ReservasHotel.Repositories.Interfaces;

public interface IQuartoRepository
{
    Task<List<Quarto>> ObterTodosAsync();
    Task<Quarto?> ObterPorIdAsync(int id);
    Task<List<Quarto>> ObterDisponiveisAsync(DateTime entrada, DateTime saida);
    Task AdicionarAsync(Quarto quarto);
    Task AtualizarAsync(Quarto quarto);
    Task RemoverAsync(int id);
}