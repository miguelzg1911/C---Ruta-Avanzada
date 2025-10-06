namespace BibliotecaDigital.Models;

public class Libros
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public int EjemplaresDisponibles { get; set; }
    public ICollection<Prestamos> prestamos { get; set; } = new List<Prestamos>();
}