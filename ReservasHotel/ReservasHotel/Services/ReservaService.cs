using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _repository;

    public ReservaService(IReservaRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Reserva>> ObterTodasAsync() => _repository.ObterTodasAsync();

    public Task<Reserva?> ObterPorIdAsync(int id) => _repository.ObterPorIdAsync(id);

    public async Task CriarAsync(Reserva reserva)
    {
        // Regra 1: período válido
        if (reserva.DataSaida <= reserva.DataEntrada)
            throw new InvalidOperationException("A data de saída deve ser posterior à data de entrada.");

        if (reserva.DataEntrada.Date < DateTime.Today)
            throw new InvalidOperationException("A data de entrada não pode estar no passado.");

        // Regra 2: sem conflito de período no mesmo quarto
        if (await _repository.ExisteConflitoAsync(reserva.QuartoId, reserva.DataEntrada, reserva.DataSaida))
            throw new InvalidOperationException("Este quarto já possui uma reserva nesse período.");

        reserva.Cancelada = false;
        await _repository.AdicionarAsync(reserva);
    }

    public async Task AtualizarAsync(Reserva reserva)
    {
        if (reserva.DataSaida <= reserva.DataEntrada)
            throw new InvalidOperationException("A data de saída deve ser posterior à data de entrada.");

        if (await _repository.ExisteConflitoAsync(reserva.QuartoId, reserva.DataEntrada, reserva.DataSaida, reserva.Id))
            throw new InvalidOperationException("Este quarto já possui uma reserva nesse período.");

        await _repository.AtualizarAsync(reserva);
    }

    public async Task CancelarAsync(int id)
    {
        var reserva = await _repository.ObterPorIdAsync(id)
            ?? throw new InvalidOperationException("Reserva não encontrada.");

        if (reserva.Cancelada)
            throw new InvalidOperationException("Esta reserva já foi cancelada.");

        reserva.Cancelada = true;
        await _repository.AtualizarAsync(reserva);
    }
}