using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.Models;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Controllers;

public class TiposQuartoController : Controller
{
    private readonly ITipoQuartoService _service;

    public TiposQuartoController(ITipoQuartoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index() => View(await _service.ObterTodosAsync());

    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        return item == null ? NotFound() : View(item);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TipoQuarto tipo)
    {
        if (!ModelState.IsValid) return View(tipo);
        await _service.CriarAsync(tipo);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TipoQuarto tipo)
    {
        if (id != tipo.Id) return NotFound();
        if (!ModelState.IsValid) return View(tipo);
        await _service.AtualizarAsync(tipo);
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
            TempData["Erro"] = "Não é possível excluir: existem quartos deste tipo.";
        }
        return RedirectToAction(nameof(Index));
    }
}