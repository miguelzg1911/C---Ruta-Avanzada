using PlataformaEducativa.Domain.Entities;

namespace PlataformaEducativa.Domain.Interfaces;

public interface ITeacherRepository
{
    Task<IEnumerable<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(int id);
    Task AddAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}