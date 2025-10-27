namespace SimulacroPrueba.Models;

public class ReservasVuelos
{
    public int Id { get; set; }
    public string NombrePasajero { get; set; }
    public string CodigoVuelo { get; set; }
    public string Origen_Destino { get; set; }
    public DateTime Fecha_Hora { get; set; }
    public string AsientoAsignado { get; set; }
    public string CodigoVuelov { get; set; }

    public List<Pasajeros> pasajeros { get; set; } = new List<Pasajeros>();
}