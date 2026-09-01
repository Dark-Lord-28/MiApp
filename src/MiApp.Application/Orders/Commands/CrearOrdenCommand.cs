using MediatR;
using MiApp.Application.DTOs;
using MiApp.Domain.Entities;

namespace MiApp.Application.Orders.Commands;

public record CrearOrdenCommand(CrearOrdenDto Dto) : IRequest<Orden>;