namespace MiApp.WebApi.Controllers;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs.Orders;
using MiApp.Application.Orders.Commands;

[ApiController]
[Authorize]
[Route("api/[controller]")] 
public class OrdenesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdenesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CrearOrden([FromBody] CrearOrdenDto dto)
    {
        var command = new CrearOrdenCommand(dto.UsuarioId, dto.MontoTotal);
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }
}