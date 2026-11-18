using ReservasHotel.Models;

namespace ReservasHotel.Repositories.Interfaces;

public interface IReservaRepository
{
    Task<List<Reserva>> ObterTodasAsync();
    Task<Reserva?> ObterPorIdAsync(int id);
    Task<bool> ExisteConflitoAsync(int quartoId, DateTime entrada, DateTime saida, int? ignorarReservaId = null);
    Task AdicionarAsync(Reserva reserva);
    Task AtualizarAsync(Reserva reserva);
}