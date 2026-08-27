namespace MiApp.Domain.Entities;

public class Orden
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public decimal MontoTotal { get; private set; }
    public string Estado { get; private set; } = "Pending"; // Pending | Paid | PaymentRejected
    public string? TransactionId { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Orden() { }

    public Orden(int usuarioId, decimal montoTotal)
    {
        if (usuarioId <= 0)
            throw new ArgumentException("El UsuarioId no es válido.", nameof(usuarioId));

        if (montoTotal <= 0)
            throw new ArgumentException("El monto total debe ser mayor a 0.", nameof(montoTotal));

        UsuarioId = usuarioId;
        MontoTotal = montoTotal;
        FechaCreacion = DateTime.UtcNow;
        Estado = "Pending";
    }

    public void MarcarComoPagada(string transactionId)
    {
        Estado = "Paid";
        TransactionId = transactionId;
    }

    public void MarcarComoRechazada()
    {
        Estado = "PaymentRejected";
    }
}