using ReservasHotel.Models;

namespace ReservasHotel.Repositories.Interfaces;

public interface IHospedeRepository
{
    Task<List<Hospede>> ObterTodosAsync();
    Task<Hospede?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Hospede hospede);
    Task AtualizarAsync(Hospede hospede);
    Task RemoverAsync(int id);
}