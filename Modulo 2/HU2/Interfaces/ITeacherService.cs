using DefaultNamespace;

namespace HU2.Interfaces;

public interface ITeacherService
{
    Task<IEnumerable<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(int id);
    Task<Teacher> CreateAsync(Teacher teacher);
    Task<bool> UpdateAsync(Teacher teacher);
    Task<bool> DeleteAsync(int id);
}