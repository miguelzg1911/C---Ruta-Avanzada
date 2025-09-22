namespace TallerSemana2.Models;

public class Concerts
{
    public int idConcierto { get; set; }
    public int idCliente { get; set; }
    public string cantante { get; set; }
    public string lugar { get; set; }
    public DateOnly fecha { get; set; }
}