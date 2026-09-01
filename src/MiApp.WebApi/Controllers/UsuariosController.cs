using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs;
using MiApp.Application.Usuarios.Commands;
using MiApp.Application.Usuarios.Queries;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new ObtenerUsuariosQuery());
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CrearUsuarioDto dto)
    {
        var command = new CrearUsuarioCommand(dto.Nombre, dto.Email, dto.Password);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] ActualizarUsuarioDto dto)
    {
        try
        {
            var exito = await _mediator.Send(new ActualizarUsuarioCommand(id, dto));
            if (!exito)
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var exito = await _mediator.Send(new EliminarUsuarioCommand(id));
        if (!exito)
            return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });

        return NoContent();
    }
}