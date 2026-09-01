namespace MiApp.Application.DTOs.Payments;

public record ProcesarPagoResponseDto(string Status, string? TransactionId = null)
{
    public bool Exito => string.Equals(Status, "Approved", StringComparison.OrdinalIgnoreCase);
}