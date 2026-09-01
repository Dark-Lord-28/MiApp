using MediatR;
using MiApp.Domain.Entities;

namespace MiApp.Application.Productos.Queries;

public record ObtenerProductosQuery() : IRequest<IEnumerable<Producto>>;