namespace DefaultNamespace;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string InstitutionalEmail { get; set; } = string.Empty;
    public string Speciality { get; set; } = string.Empty;
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
}