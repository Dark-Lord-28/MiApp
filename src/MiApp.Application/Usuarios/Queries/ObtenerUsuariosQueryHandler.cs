using MediatR;
using MiApp.Application.DTOs;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Usuarios.Queries;

public class ObtenerUsuariosQueryHandler : IRequestHandler<ObtenerUsuariosQuery, IEnumerable<UsuarioDto>>
{
    private readonly IUsuarioRepository _repository;

    public ObtenerUsuariosQueryHandler(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioDto>> Handle(ObtenerUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Email = u.Email,
            Rol = u.Rol,
            FechaCreacion = u.FechaCreacion
        });
    }
}