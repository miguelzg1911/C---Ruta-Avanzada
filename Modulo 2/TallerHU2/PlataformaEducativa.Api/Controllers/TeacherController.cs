using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Application.Interfaces;
using PlataformaEducativa.Domain.Entities;

namespace PlataformaEducativa.Api.Controllers;

public class TeacherController : ControllerBase
{
    private readonly ITeacherService _service;

    public TeacherController(ITeacherService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var student = await _service.GetAllAsync();
        return Ok(student);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _service.GetByIdAsync(id);
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Teacher teacher)
    {
        var created = await _service.CreateAsync(teacher);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
    {
        var updated = await _service.UpdateAsync(id, teacher);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}