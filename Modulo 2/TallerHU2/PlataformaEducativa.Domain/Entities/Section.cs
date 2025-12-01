namespace PlataformaEducativa.Domain.Entities;

public class Section
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
    
    public int DayOfWeek { get; set; } 
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public string Room { get; set; } = string.Empty;
    
    public int Capacity { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}