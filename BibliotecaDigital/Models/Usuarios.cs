namespace BibliotecaDigital.Models;

public class Usuarios
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    public ICollection<Prestamos> prestamos { get; set; } = new List<Prestamos>();
}