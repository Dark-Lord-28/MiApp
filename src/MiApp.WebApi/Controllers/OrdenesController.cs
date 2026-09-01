using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs;
using MiApp.Application.Orders.Commands;
using MiApp.Application.Orders.Queries;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdenesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdenesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var orden = await _mediator.Send(new ObtenerOrdenPorIdQuery(id));
        if (orden == null)
            return NotFound(new { mensaje = $"No se encontró la orden con ID {id}." });

        return Ok(orden);
    }

    [HttpPost]
    public async Task<IActionResult> CrearOrden([FromBody] CrearOrdenDto dto)
    {
        try
        {
            var orden = await _mediator.Send(new CrearOrdenCommand(dto));
            return Ok(orden);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }
}