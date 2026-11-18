using ReservasHotel.Models;
using ReservasHotel.Repositories.Interfaces;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Services;

public class QuartoService : IQuartoService
{
    private readonly IQuartoRepository _repository;

    public QuartoService(IQuartoRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Quarto>> ObterTodosAsync() => _repository.ObterTodosAsync();

    public Task<Quarto?> ObterPorIdAsync(int id) => _repository.ObterPorIdAsync(id);

    public Task<List<Quarto>> ObterDisponiveisAsync(DateTime entrada, DateTime saida)
    {
        if (saida <= entrada)
            throw new InvalidOperationException("A data de saída deve ser posterior à data de entrada.");

        return _repository.ObterDisponiveisAsync(entrada, saida);
    }

    public Task CriarAsync(Quarto quarto) => _repository.AdicionarAsync(quarto);

    public Task AtualizarAsync(Quarto quarto) => _repository.AtualizarAsync(quarto);

    public Task RemoverAsync(int id) => _repository.RemoverAsync(id);
}