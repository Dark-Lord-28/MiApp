namespace MiApp.Application.Interfaces;

using MiApp.Application.DTOs;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}