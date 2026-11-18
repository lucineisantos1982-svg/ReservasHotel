using System.ComponentModel.DataAnnotations;

namespace ReservasHotel.Models;

public class Hospede
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o e-mail")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public List<Reserva> Reservas { get; set; } = new();

    public bool Ativo { get; set; } = true;
}