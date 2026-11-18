using ReservasHotel.Models;

namespace ReservasHotel.Services.Interfaces;

public interface IReservaService
{
    Task<List<Reserva>> ObterTodasAsync();
    Task<Reserva?> ObterPorIdAsync(int id);
    Task CriarAsync(Reserva reserva);
    Task AtualizarAsync(Reserva reserva);
    Task CancelarAsync(int id);
}