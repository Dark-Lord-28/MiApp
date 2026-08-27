namespace MiApp.Application.DTOs.Orders;

public class OrdenResponseDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public decimal MontoTotal { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? TransactionId { get; set; }
    public DateTime FechaCreacion { get; set; }
}