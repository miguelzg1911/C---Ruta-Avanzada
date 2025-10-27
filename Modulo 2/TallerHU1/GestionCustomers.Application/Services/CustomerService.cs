using GestionCustomers.Domain.Interfaces;
using GestionCustomers.Domain.Models;

namespace GestionCustomers.Application.Services;

public class CustomerService
{
    private readonly IGenericRepository<Customer> _repository;

    public CustomerService(IGenericRepository<Customer> repository)
    {
        _repository = repository;
    }
    
    // Listar los clientes
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    
    // Buscar por Id
    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    
    // Agregar Cliente
    public async Task<Customer> CreateAsync(Customer createCustomer)
    {
        await _repository.AddAsync(createCustomer);
        await _repository.SaveChangesAsync();
        return createCustomer;
    }
    
    // Actualizar Cliente
    public async Task<bool> UpdateAsync(int id, Customer updateCustomer)
    {
        var exists = await _repository.GetByIdAsync(id);
        if (exists == null) return false;
        
        exists.Name = updateCustomer.Name;
        exists.Email = updateCustomer.Email;
        
        await _repository.UpdateAsync(exists);
        await _repository.SaveChangesAsync();
        return true;
    }
    
    // Eliminar Cliente 
    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.GetByIdAsync(id);
        if (exists == null) return false;
        
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }
}