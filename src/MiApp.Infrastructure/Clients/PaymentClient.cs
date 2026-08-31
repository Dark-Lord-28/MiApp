namespace MiApp.Infrastructure.Clients;

using System.Net.Http.Json;
using MiApp.Application.DTOs.Payments;
using MiApp.Application.Interfaces;

public class PaymentClient : IPaymentClient
{
    private readonly HttpClient _httpClient;

    public PaymentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProcesarPagoResponseDto?> ProcesarPagoAsync(string orderId, decimal amount)
    {
        try
        {
            var payload = new { OrderId = orderId, Amount = amount };
            var response = await _httpClient.PostAsJsonAsync("/api/payments/process", payload);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProcesarPagoResponseDto>();
        }
        catch
        {
            // Si PaymentService está caido o da timeout
            return null;
        }
    }
} 