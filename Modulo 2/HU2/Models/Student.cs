namespace DefaultNamespace;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string InstitutionalEmail { get; set; } = string.Empty;
    
    public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
    public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
}