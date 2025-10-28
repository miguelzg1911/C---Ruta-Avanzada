using GestionCustomers.Domain.Interfaces;
using GestionCustomers.Domain.Models;
using GestionCustomers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionCustomers.Application.Services;

public class OrderService
{
    private readonly IGenericRepository<Order> _repository;
    private readonly IGenericRepository<OrderDetail> _detailRepository;
    private readonly AppDbContext _context;

    public OrderService(IGenericRepository<Order> repository, 
        IGenericRepository<OrderDetail> detailRepository,
        AppDbContext context)
    {
        _detailRepository = detailRepository;
        _repository = repository;
        _context = context;
    }
    
    // Listar Ordernes
    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .ToListAsync();
    }
    
    // Buscar por Id
    public async Task<Order?> GetByIdAsync(int id)
    {   
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
    
    // Agregar Orden
    public async Task<Order> CreateAsync(Order createOrder)
    {
        createOrder.OrderDate = DateTime.Now;
        createOrder.Status = string.IsNullOrEmpty(createOrder.Status) ? "Pendiente" : createOrder.Status;
        
        await _repository.AddAsync(createOrder);
        await _repository.SaveChangesAsync();
            
        return createOrder;
    }
    
    // Actualizar Orden
    public async Task<bool> updateOrder(int id, Order updateOrder)
    {
        // Buscamos la orden con sus detalles actuales
        var exists = await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (exists == null)
            return false;
        
        exists.Status = updateOrder.Status;
        exists.CustomerId = updateOrder.CustomerId;
        exists.OrderDate = DateTime.Now;
        
        if (updateOrder.OrderDetails != null && updateOrder.OrderDetails.Any())
        {
            foreach (var detail in updateOrder.OrderDetails)
            {
                // Buscamos si ese detalle ya existe
                var existingDetail = exists.OrderDetails.FirstOrDefault(d => d.Id == detail.Id);

                if (existingDetail != null)
                {
                    existingDetail.ProductName = detail.ProductName;
                    existingDetail.Quantity = detail.Quantity;
                    existingDetail.UnitPrice = detail.UnitPrice;
                }
                else
                {
                    detail.OrderId = exists.Id;
                    _context.OrderDetails.Add(detail);
                }
            }
        }
        await _context.SaveChangesAsync();

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