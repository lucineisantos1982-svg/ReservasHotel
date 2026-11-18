using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Services;

public class TipoQuartoService : ITipoQuartoService
{
    private readonly ITipoQuartoRepository _repository;

    public TipoQuartoService(ITipoQuartoRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TipoQuarto>> ObterTodosAsync() => _repository.ObterTodosAsync();

    public Task<TipoQuarto?> ObterPorIdAsync(int id) => _repository.ObterPorIdAsync(id);

    public Task CriarAsync(TipoQuarto tipo) => _repository.AdicionarAsync(tipo);

    public Task AtualizarAsync(TipoQuarto tipo) => _repository.AtualizarAsync(tipo);

    public Task RemoverAsync(int id) => _repository.RemoverAsync(id);
}