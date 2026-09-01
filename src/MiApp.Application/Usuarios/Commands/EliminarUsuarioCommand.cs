using MediatR;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Usuarios.Commands;

public record EliminarUsuarioCommand(int Id) : IRequest<bool>;

public class EliminarUsuarioCommandHandler : IRequestHandler<EliminarUsuarioCommand, bool>
{
    private readonly IUsuarioRepository _usuarioRepository;

    public EliminarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
        if (usuario == null)
            return false;

        _usuarioRepository.Delete(usuario);
        await _usuarioRepository.SaveChangesAsync();
        return true;
    }
}