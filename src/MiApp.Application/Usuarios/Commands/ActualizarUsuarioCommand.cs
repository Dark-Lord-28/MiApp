using MediatR;
using MiApp.Application.DTOs;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Usuarios.Commands;

public record ActualizarUsuarioCommand(int Id, ActualizarUsuarioDto Dto) : IRequest<bool>;

public class ActualizarUsuarioCommandHandler : IRequestHandler<ActualizarUsuarioCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ActualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
        if (usuario == null)
            return false;

        // Actualización de datos del usuario
        usuario.ActualizarDatos(request.Dto.Nombre, request.Dto.Email);

        await _usuarioRepository.SaveChangesAsync();
        return true;
    }
}