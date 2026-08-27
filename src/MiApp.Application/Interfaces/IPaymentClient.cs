namespace MiApp.Application.Interfaces;

using MiApp.Application.DTOs.Payments;

public interface IPaymentClient
{
    Task<ProcesarPagoResponseDto?> ProcesarPagoAsync(string orderId, decimal amount);
}