using MediatR;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Orders.Commands;

public class CrearOrdenCommandHandler : IRequestHandler<CrearOrdenCommand, Orden>
{
    private readonly IOrdenRepository _ordenRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IPaymentClient _paymentClient;

    public CrearOrdenCommandHandler(
        IOrdenRepository ordenRepository,
        IProductoRepository productoRepository,
        IPaymentClient paymentClient)
    {
        _ordenRepository = ordenRepository;
        _productoRepository = productoRepository;
        _paymentClient = paymentClient;
    }

    public async Task<Orden> Handle(CrearOrdenCommand request, CancellationToken cancellationToken)
    {
        var orden = new Orden(request.Dto.UsuarioId);

        foreach (var item in request.Dto.Items)
        {
            var producto = await _productoRepository.GetByIdAsync(item.ProductoId);
            if (producto == null)
                throw new KeyNotFoundException($"El producto con ID {item.ProductoId} no fue encontrado.");

            orden.AgregarItem(producto, item.Cantidad);
        }

        var cobroExitoso = await _paymentClient.ProcesarPagoAsync(orden.Id.ToString(), orden.MontoTotal);

        if (cobroExitoso != null && cobroExitoso.Exito)
        {
            orden.MarcarComoPagada(cobroExitoso.TransactionId ?? Guid.NewGuid().ToString());
        }
        else
        {
            orden.MarcarComoRechazada();
        }

        await _ordenRepository.AddAsync(orden);
        await _ordenRepository.SaveChangesAsync();

        return orden;
    }
}