using MediatR;
using MiApp.Application.DTOs;

namespace MiApp.Application.Usuarios.Commands;

public record CrearUsuarioCommand(
    string Nombre, 
    string Email, 
    string Password
) : IRequest<UsuarioDto>;