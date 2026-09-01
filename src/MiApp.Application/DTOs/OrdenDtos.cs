namespace MiApp.Application.DTOs;

public record ItemOrdenDto(int ProductoId, int Cantidad);
public record CrearOrdenDto(int UsuarioId, List<ItemOrdenDto> Items);