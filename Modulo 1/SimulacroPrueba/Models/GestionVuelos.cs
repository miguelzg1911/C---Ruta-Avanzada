namespace SimulacroPrueba.Models;

public class GestionVuelos
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public DateTime Salida { get; set; }
    public DateTime Llegada { get; set; }
    public int SillasDisponible { get; set; }
    
    public List<ReservasVuelos> reservasVuelos { get; set; } = new List<ReservasVuelos>();
}