using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.Models;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Controllers;

public class HospedesController : Controller
{
    private readonly IHospedeService _service;

    public HospedesController(IHospedeService service)
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
    public async Task<IActionResult> Create(Hospede hospede)
    {
        if (!ModelState.IsValid) return View(hospede);
        await _service.CriarAsync(hospede);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Hospede hospede)
    {
        if (id != hospede.Id) return NotFound();
        if (!ModelState.IsValid) return View(hospede);
        await _service.AtualizarAsync(hospede);
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
            TempData["Erro"] = "Não é possível excluir: o hóspede possui reservas.";
        }
        return RedirectToAction(nameof(Index));
    }
}