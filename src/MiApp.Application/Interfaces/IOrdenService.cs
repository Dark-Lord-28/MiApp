namespace MiApp.Application.Interfaces;

using MiApp.Application.DTOs.Orders;

public interface IOrdenService
{
    Task<OrdenResponseDto> CrearOrdenAsync(CrearOrdenDto dto);
}