using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.Models;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Controllers;

public class QuartosController : Controller
{
    private readonly IQuartoService _service;
    private readonly ITipoQuartoService _tipoService;

    public QuartosController(IQuartoService service, ITipoQuartoService tipoService)
    {
        _service = service;
        _tipoService = tipoService;
    }

    private async Task CarregarTiposAsync(int? tipoId = null)
    {
        ViewBag.TiposQuarto = new SelectList(await _tipoService.ObterTodosAsync(), "Id", "Nome", tipoId);
    }

    public async Task<IActionResult> Index() => View(await _service.ObterTodosAsync());

    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        return item == null ? NotFound() : View(item);
    }

    public async Task<IActionResult> Disponiveis(DateTime? entrada, DateTime? saida)
    {
        ViewBag.Entrada = entrada;
        ViewBag.Saida = saida;

        if (entrada == null || saida == null)
            return View(new List<Quarto>());

        try
        {
            return View(await _service.ObterDisponiveisAsync(entrada.Value, saida.Value));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(new List<Quarto>());
        }
    }

    public async Task<IActionResult> Create()
    {
        await CarregarTiposAsync();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Quarto quarto)
    {
        if (!ModelState.IsValid)
        {
            await CarregarTiposAsync(quarto.TipoQuartoId);
            return View(quarto);
        }
        await _service.CriarAsync(quarto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        if (item == null) return NotFound();
        await CarregarTiposAsync(item.TipoQuartoId);
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Quarto quarto)
    {
        if (id != quarto.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            await CarregarTiposAsync(quarto.TipoQuartoId);
            return View(quarto);
        }
        await _service.AtualizarAsync(quarto);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _service.RemoverAsync(id);
        }
        catch (DbUpdateException)
        {
            TempData["Erro"] = "Não é possível excluir: o quarto possui reservas.";
        }
        return RedirectToAction(nameof(Index));
    }
}