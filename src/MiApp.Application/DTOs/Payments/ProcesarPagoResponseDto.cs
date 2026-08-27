namespace MiApp.Application.DTOs.Payments;

public class ProcesarPagoResponseDto
{
    public string Status { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
}