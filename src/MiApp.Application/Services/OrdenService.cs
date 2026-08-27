namespace MiApp.Application.Services;

using MiApp.Application.DTOs.Orders;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

public class OrdenService : IOrdenService
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IPaymentClient _paymentClient;

    public OrdenService(IOrdenRepository ordenRepository, IPaymentClient paymentClient)
    {
        _ordenRepository = ordenRepository;
        _paymentClient = paymentClient;
    }

    public async Task<OrdenResponseDto> CrearOrdenAsync(CrearOrdenDto dto)
    {
        // 1. Crear la orden inicialmente en estado Pending
        var orden = new Orden(dto.UsuarioId, dto.MontoTotal);
        await _ordenRepository.AddAsync(orden);

        // 2. Comunicarse con PaymentService vía HTTP (IHttpClientFactory)
        var pagoResult = await _paymentClient.ProcesarPagoAsync($"ORD-{orden.Id}", orden.MontoTotal);

        // 3. Evaluar respuesta del microservicio
        if (pagoResult != null && pagoResult.Status == "Approved")
        {
            orden.MarcarComoPagada(pagoResult.TransactionId);
        }
        else
        {
            orden.MarcarComoRechazada();
        }

        // 4. Actualizar estado en SQLite
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