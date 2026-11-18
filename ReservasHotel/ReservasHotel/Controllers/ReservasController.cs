using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReservasHotel.Models;
using ReservasHotel.Services.Interfaces;

namespace ReservasHotel.Controllers;

public class ReservasController : Controller
{
    private readonly IReservaService _service;
    private readonly IHospedeService _hospedeService;
    private readonly IQuartoService _quartoService;

    public ReservasController(IReservaService service, IHospedeService hospedeService, IQuartoService quartoService)
    {
        _service = service;
        _hospedeService = hospedeService;
        _quartoService = quartoService;
    }

    private async Task CarregarListasAsync(int? hospedeId = null, int? quartoId = null)
    {
        ViewBag.Hospedes = new SelectList(await _hospedeService.ObterTodosAsync(), "Id", "Nome", hospedeId);
        ViewBag.Quartos = await _quartoService.ObterTodosAsync();
    }

    public async Task<IActionResult> Index() => View(await _service.ObterTodasAsync());

    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        return item == null ? NotFound() : View(item);
    }

    public async Task<IActionResult> Create()
    {
        await CarregarListasAsync();
        return View(new Reserva
        {
            DataEntrada = DateTime.Today,
            DataSaida = DateTime.Today.AddDays(1)
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reserva reserva)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.CriarAsync(reserva);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        await CarregarListasAsync(reserva.HospedeId, reserva.QuartoId);
        return View(reserva);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        if (item == null) return NotFound();
        await CarregarListasAsync(item.HospedeId, item.QuartoId);
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reserva reserva)
    {
        if (id != reserva.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _service.AtualizarAsync(reserva);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        await CarregarListasAsync(reserva.HospedeId, reserva.QuartoId);
        return View(reserva);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancelar(int id)
    {
        try
        {
            await _service.CancelarAsync(id);
        }
        catch (InvalidOperationException ex)
        {
            TempData["Erro"] = ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
}