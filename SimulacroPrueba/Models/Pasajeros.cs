namespace SimulacroPrueba.Models;

public class Pasajeros
{
    public int Id { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Documento { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }

    public List<ReservasVuelos> reservasVuelos { get; set; } = new List<ReservasVuelos>();
}