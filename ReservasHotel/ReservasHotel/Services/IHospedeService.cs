using ReservasHotel.Models;

namespace ReservasHotel.Services.Interfaces;

public interface IHospedeService
{
    Task<List<Hospede>> ObterTodosAsync();
    Task<Hospede?> ObterPorIdAsync(int id);
    Task CriarAsync(Hospede hospede);
    Task AtualizarAsync(Hospede hospede);
    Task RemoverAsync(int id);
}