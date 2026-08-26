namespace MiApp.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var resultado = await _authService.LoginAsync(dto);
        if (resultado == null)
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        return Ok(resultado);
    }
}