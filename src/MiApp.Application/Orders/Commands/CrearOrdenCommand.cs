namespace MiApp.Application.Orders.Commands;

using MediatR;
using MiApp.Application.DTOs.Orders;

public record CrearOrdenCommand(int UsuarioId, decimal MontoTotal) : IRequest<OrdenResponseDto>;