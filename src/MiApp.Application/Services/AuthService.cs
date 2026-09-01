using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
        if (usuario == null) return null;

        bool esValida = _passwordHasher.Verify(dto.Password, usuario.PasswordHash);
        if (!esValida) return null;

        var token = _jwtProvider.GenerateToken(usuario);
        return new AuthResponseDto(token);
    }
}