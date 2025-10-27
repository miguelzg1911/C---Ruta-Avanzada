using GestionCustomers.Domain.Interfaces;
using GestionCustomers.Domain.Models;

namespace GestionCustomers.Application.Services;

public class OrderService
{
    private readonly IGenericRepository<Order> _repository;
    private readonly IGenericRepository<OrderDetail> _detailRepository;

    public OrderService(IGenericRepository<Order> repository, 
        IGenericRepository<OrderDetail> detailRepository)
    {
        _detailRepository = detailRepository;
        _repository = repository;
    }
    
    // Listar Ordernes
    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _repository.GetAllAsync();
    }
    
    // Buscar por Id
    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    
    // Agregar Orden
    public async Task<Order> CreateAsync(Order createOrder)
    {
        createOrder.OrderDate = DateTime.Now;
        createOrder.Status = string.IsNullOrEmpty(createOrder.Status) ? "Pendiente" : createOrder.Status;
        
        await _repository.AddAsync(createOrder);
        await _repository.SaveChangesAsync();

        if (createOrder.OrderDetails != null && createOrder.OrderDetails.Any())
        {
            foreach (var detail in createOrder.OrderDetails)
            {
                detail.OrderId = createOrder.Id;
                await _detailRepository.AddAsync(detail);
            }

            await _detailRepository.SaveChangesAsync();
        }
        return createOrder;
    }
    
    // Actualizar Orden
    public async Task<bool> updateOrder(int id, Order updateOrder)
    {
        var exists = await _repository.GetByIdAsync(id);
        if (exists == null)
            return false;

        exists.OrderDate = updateOrder.OrderDate;
        exists.Status = updateOrder.Status;
        exists.CustomerId = updateOrder.CustomerId;

        if (updateOrder.OrderDetails != null)
        {
            foreach (var detail in updateOrder.OrderDetails)
            {
                if (detail.Id == 0)
                {
                    detail.Order.Id = id;
                    await _detailRepository.AddAsync(detail);
                }
                else
                {
                    await _detailRepository.UpdateAsync(detail);
                }
            }
        }

        await _repository.UpdateAsync(exists);
        await _repository.SaveChangesAsync();
        await _detailRepository.SaveChangesAsync();

        return true;
    }

    // Eliminar Orden
    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _repository.GetByIdAsync(id);
        if (exists == null) return false;

        var details = await _detailRepository.GetAllAsync();
        foreach (var d in details.Where(d => d.OrderId == id))
        {
            await _detailRepository.DeleteAsync(d.Id);
        }

        await _repository.DeleteAsync(id);
        await _detailRepository.SaveChangesAsync();
        await _repository.SaveChangesAsync();

        return true;
    }
}