using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Productos.Queries;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var productos = await _mediator.Send(new ObtenerProductosQuery());
        return Ok(productos);
    }
}