namespace PlataformaEducativa.Domain.Entities;

public class Qualification
{
    public int Id { get; set; }
    
    public int InscriptionId { get; set; }
    public Inscription? Inscription { get; set; }
    
    public decimal Value { get; set; }

    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}