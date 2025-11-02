using DefaultNamespace;
using HU2.Interfaces;

namespace HU2.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository repository)
    {
        _studentRepository = repository;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        try
        {
            return await _studentRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los estudiantes", ex);
        }
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        try
        {
            var exists = await _studentRepository.GetByIdAsync(id);
            if (exists != null)
                throw new Exception("El estudiante no existe");

            return exists;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener los estudiantes", ex);
        }
    }

    public async Task<Student> CreateAsync(Student student)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.Document) ||
                string.IsNullOrWhiteSpace(student.InstitutionalEmail) ||
                string.IsNullOrWhiteSpace(student.Phone))
            {
                throw new Exception("Los campos son obligatorios");
            }

            var exists = await _studentRepository.GetAllAsync();
            if (exists.Any(e => e.InstitutionalEmail == student.InstitutionalEmail))
            {
                throw new Exception("El correo institucional ya esta registrado");
            }

            await _studentRepository.AddAsync(student);
            _studentRepository.SaveChangesAsync();
            
            return student;
        }
        catch (ArgumentException ex)
        {
            throw new Exception($"Error de validacion: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception("Error al crear el estudiante", ex);
        }
    }

    public async Task<bool> UpdateAsync(Student student)
    {
        try
        {
            var exists = await _studentRepository.GetByIdAsync(student.Id);
            if (exists == null)
                throw new Exception("El estudiante no existe");

            if (string.IsNullOrWhiteSpace(student.Name))
                throw new Exception("El nombre no puede estar vacio");
            
            exists.Name = student.Name;
            exists.Document = student.Document;
            exists.InstitutionalEmail = student.InstitutionalEmail;
            exists.Phone = student.Phone;
            
            await _studentRepository.UpdateAsync(exists);
            _studentRepository.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar el estudiante: {ex.Message}");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var delete = await _studentRepository.DeleteAsync(id);
            if (!delete)
                throw new Exception("No se pudo eliminar el estudiante");
            
            await _studentRepository.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new  Exception("Error al eliminar el estudiante", ex);
        }
    }
}