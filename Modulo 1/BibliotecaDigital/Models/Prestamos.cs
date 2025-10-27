namespace BibliotecaDigital.Models;

public class Prestamos
{
    public int Id { get; set; }

    public DateTime FechaPrestamo { get; set; } = DateTime.Now; 

    public DateTime? FechaDevolucion { get; set; }  

    // Relación con Usuario
    public int IdUsuario { get; set; }
    public Usuarios? Usuario { get; set; }

    // Relación con Libro
    public int IdLibro { get; set; }
    public Libros? Libro { get; set; }
}