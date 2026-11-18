using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Services;

public class HospedeService : IHospedeService
{
    private readonly IHospedeRepository _repository;

    public HospedeService(IHospedeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Hospede>> ObterTodosAsync() => _repository.ObterTodosAsync();

    public Task<Hospede?> ObterPorIdAsync(int id) => _repository.ObterPorIdAsync(id);

    public async Task CriarAsync(Hospede hospede)
    {
        hospede.Nome = hospede.Nome.Trim();
        hospede.Email = hospede.Email.Trim().ToLower();
        await _repository.AdicionarAsync(hospede);
    }

    public async Task AtualizarAsync(Hospede hospede)
    {
        hospede.Nome = hospede.Nome.Trim();
        hospede.Email = hospede.Email.Trim().ToLower();
        await _repository.AtualizarAsync(hospede);
    }

    public Task RemoverAsync(int id) => _repository.RemoverAsync(id);
}