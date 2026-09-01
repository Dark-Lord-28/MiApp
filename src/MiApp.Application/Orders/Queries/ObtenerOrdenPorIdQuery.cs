using MediatR;
using MiApp.Domain.Entities;

namespace MiApp.Application.Orders.Queries;

public record ObtenerOrdenPorIdQuery(int Id) : IRequest<Orden?>;