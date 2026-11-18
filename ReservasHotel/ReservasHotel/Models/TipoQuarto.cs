using System.ComponentModel.DataAnnotations;

namespace ReservasHotel.Models;

public class TipoQuarto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome")]
    [StringLength(50)]
    public string Nome { get; set; } = string.Empty;

    public List<Quarto> Quartos { get; set; } = new();
}