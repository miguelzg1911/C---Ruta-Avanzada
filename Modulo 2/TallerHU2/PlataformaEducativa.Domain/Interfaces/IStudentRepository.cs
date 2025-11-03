using PlataformaEducativa.Domain.Entities;

namespace PlataformaEducativa.Domain.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(int id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}