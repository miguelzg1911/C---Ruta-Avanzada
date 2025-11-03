namespace PlataformaEducativa.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public ICollection<Section> Sections { get; set; } = new List<Section>();
}