using MediatR;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Orders.Queries;

public class ObtenerOrdenPorIdQueryHandler : IRequestHandler<ObtenerOrdenPorIdQuery, Orden?>
{
    private readonly IOrdenRepository _ordenRepository;

    public ObtenerOrdenPorIdQueryHandler(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public async Task<Orden?> Handle(ObtenerOrdenPorIdQuery request, CancellationToken cancellationToken)
    {
        return await _ordenRepository.GetByIdAsync(request.Id);
    }
}