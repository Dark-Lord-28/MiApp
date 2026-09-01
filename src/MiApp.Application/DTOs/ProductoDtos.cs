namespace MiApp.Application.DTOs;

public record CrearProductoDto(string Nombre, decimal Precio, int Stock);
public record ProductoDto(int Id, string Nombre, decimal Precio, int Stock);