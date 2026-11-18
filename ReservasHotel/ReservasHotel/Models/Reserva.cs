using System.ComponentModel.DataAnnotations;

namespace ReservasHotel.Models;

public class Reserva
{
    public int Id { get; set; }

    public int HospedeId { get; set; }
    public Hospede? Hospede { get; set; }

    public int QuartoId { get; set; }
    public Quarto? Quarto { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataEntrada { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataSaida { get; set; }

    public bool Cancelada { get; set; }
}