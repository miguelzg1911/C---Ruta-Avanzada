using GestionCustomers.Application.Services;
using GestionCustomers.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomersController(CustomerService service)
    {
        _service = service;
    }
    
    // Get Customers
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _service.GetAllAsync();
        return Ok(customers);
    }
    
    // Get Customer by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);
        return customer is not null ? Ok(customer) : NotFound();
    }

    // Post Create Customer
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]Customer customer)
    {
        var created = await _service.CreateAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    // Put Update Customer
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
    {
        var updated = await _service.UpdateAsync(id, customer);
        return updated ? NoContent() : NotFound();
    }
    
    // Delete Customer
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}