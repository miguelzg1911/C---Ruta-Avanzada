using DefaultNamespace;
using HU2.Interfaces;

namespace HU2.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository repository)
    {
        _teacherRepository = repository;
    }

    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        try
        {
            return await _teacherRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los profesores", ex);
        }
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        try
        {
            var exists = await _teacherRepository.GetByIdAsync(id);
            if (exists == null)
                throw new Exception("El profesor no existe");
            
            return exists;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener al profesor", ex);
        }
    }

    public async Task<Teacher> CreateAsync(Teacher teacher)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(teacher.Name) ||
                string.IsNullOrWhiteSpace(teacher.Document) ||
                string.IsNullOrWhiteSpace(teacher.Phone) ||
                string.IsNullOrWhiteSpace(teacher.InstitutionalEmail) ||
                string.IsNullOrWhiteSpace(teacher.Speciality))
            {
                throw new ArgumentException("Todos los campos son obligatorios");
            }
            
            var existing = await _teacherRepository.GetAllAsync();
            if (existing.Any(t => t.InstitutionalEmail == teacher.InstitutionalEmail))
            {
                throw new ArgumentException("El correo institucional ya está registrado");
            }

            await _teacherRepository.AddAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            return teacher;
        }
        catch (ArgumentException ex)
        {
            throw new Exception($"Error de validación: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception("Error al crear el profesor", ex);
        }
    }
    
    public async Task<bool> UpdateAsync(Teacher teacher)
    {
        try
        {
            var exists = await _teacherRepository.GetByIdAsync(teacher.Id);
            if (exists == null)
                throw new Exception("El profesor no existe");
            
            if (string.IsNullOrWhiteSpace(teacher.Name))
                throw new ArgumentException("El nombre no puede estar vacío");
            if (string.IsNullOrWhiteSpace(teacher.Speciality))
                throw new ArgumentException("Debe especificar una especialidad");

            exists.Name = teacher.Name;
            exists.Document = teacher.Document;
            exists.Phone = teacher.Phone;
            exists.InstitutionalEmail = teacher.InstitutionalEmail;
            exists.Speciality = teacher.Speciality;

            await _teacherRepository.UpdateAsync(exists);
            await _teacherRepository.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar el profesor", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var delete = await _teacherRepository.DeleteAsync(id);
            if (!delete)
                throw new Exception("No se pudo eliminar el profesor");

            await _teacherRepository.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al eliminar el profesor", ex);
        }
    }
}
