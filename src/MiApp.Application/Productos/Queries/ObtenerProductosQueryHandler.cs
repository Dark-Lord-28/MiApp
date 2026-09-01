using MediatR;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Productos.Queries;

public class ObtenerProductosQueryHandler : IRequestHandler<ObtenerProductosQuery, IEnumerable<Producto>>
{
    private readonly IProductoRepository _productoRepository;

    public ObtenerProductosQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<IEnumerable<Producto>> Handle(ObtenerProductosQuery request, CancellationToken cancellationToken)
    {
        return await _productoRepository.GetAllAsync();
    }
}