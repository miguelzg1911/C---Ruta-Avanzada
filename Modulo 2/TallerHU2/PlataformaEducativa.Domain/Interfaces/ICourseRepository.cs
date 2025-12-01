using PlataformaEducativa.Domain.Entities;

namespace PlataformaEducativa.Domain.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}