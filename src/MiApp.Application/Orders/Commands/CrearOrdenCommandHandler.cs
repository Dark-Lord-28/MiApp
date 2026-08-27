namespace MiApp.Application.Orders.Commands;

using MediatR;
using MiApp.Application.DTOs.Orders;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

public class CrearOrdenCommandHandler : IRequestHandler<CrearOrdenCommand, OrdenResponseDto>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IPaymentClient _paymentClient;

    public CrearOrdenCommandHandler(IOrdenRepository ordenRepository, IPaymentClient paymentClient)
    {
        _ordenRepository = ordenRepository;
        _paymentClient = paymentClient;
    }

    public async Task<OrdenResponseDto> Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
    {
        // 1. Crear la orden inicialmente en estado Pending
        var orden = new Orden(request.UsuarioId, request.MontoTotal);
        await _ordenRepository.AddAsync(orden);

        try
        {
            // 2. Comunicarse con PaymentService vía HTTP
            var pagoResult = await _paymentClient.ProcesarPagoAsync($"ORD-{orden.Id}", orden.MontoTotal);

            // 3. Evaluar respuesta
            if (pagoResult != null && pagoResult.Status == "Approved")
            {
                orden.MarcarComoPagada(pagoResult.TransactionId);
            }
            else
            {
                orden.MarcarComoRechazada();
            }
        }
        catch (HttpRequestException)
        {
            // Manejo si el microservicio de pagos está apagado o inalcanzable
            orden.MarcarComoRechazada();
        }
        catch (TaskCanceledException)
        {
            // Manejo si ocurre un Timeout en la petición HTTP
            orden.MarcarComoRechazada();
        }

        // 4. Actualizar estado final en la base de datos
        await _ordenRepository.UpdateAsync(orden);

        return new OrdenResponseDto
        {
            Id = orden.Id,
            UsuarioId = orden.UsuarioId,
            MontoTotal = orden.MontoTotal,
            Estado = orden.Estado,
            TransactionId = orden.TransactionId,
            FechaCreacion = orden.FechaCreacion
        };
    }
}