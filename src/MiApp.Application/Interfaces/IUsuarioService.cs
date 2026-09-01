using MiApp.Application.DTOs;
using MiApp.Domain.Entities;

namespace MiApp.Application.Interfaces;

public interface IUsuarioService
{
    Task<Usuario> RegistrarUsuarioAsync(CrearUsuarioDto dto);
}