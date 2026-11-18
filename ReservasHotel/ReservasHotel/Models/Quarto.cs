using System.ComponentModel.DataAnnotations;

namespace ReservasHotel.Models;

public class Quarto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o número")]
    public string Numero { get; set; } = string.Empty;

    [Range(0.01, 100000, ErrorMessage = "Preço inválido")]
    public decimal Preco { get; set; }

    public int TipoQuartoId { get; set; }
    public TipoQuarto? TipoQuarto { get; set; }

    public bool Ativo { get; set; } = true;

    public List<Reserva> Reservas { get; set; } = new();
}