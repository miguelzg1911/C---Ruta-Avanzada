using PlataformaEducativa.Domain.Entities;

namespace PlataformaEducativa.Domain.Interfaces;

public interface ISectionRepository
{
    Task<IEnumerable<Section>> GetAllAsync();
    Task<Section?> GetByIdAsync(int id);
    Task<IEnumerable<Section>> GetByCourseIdAsync(int id);
    Task AddAsync(Section section);
    Task UpdateAsync(Section section);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasScheduleConflictAsync(int courseId, int dayOfWeek, TimeSpan start, TimeSpan end, int? excludeSectionId = null);
    Task SaveChangesAsync();
}