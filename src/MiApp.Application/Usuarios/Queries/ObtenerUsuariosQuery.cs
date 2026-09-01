using MediatR;
using MiApp.Application.DTOs;

namespace MiApp.Application.Usuarios.Queries;

public record ObtenerUsuariosQuery() : IRequest<IEnumerable<UsuarioDto>>;