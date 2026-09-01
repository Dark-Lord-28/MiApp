using MiApp.Domain.Entities;

namespace MiApp.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Usuario usuario);
}