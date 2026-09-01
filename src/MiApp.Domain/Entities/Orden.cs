namespace MiApp.Domain.Entities;

public class Orden
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public decimal MontoTotal { get; private set; }
    public string Estado { get; private set; } = "Pendiente";
    public string? TransactionId { get; private set; }
    public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;

    private readonly List<OrdenItem> _items = new();
    public IReadOnlyCollection<OrdenItem> Items => _items.AsReadOnly();

    public Orden(int usuarioId)
    {
        UsuarioId = usuarioId;
        MontoTotal = 0;
        Estado = "Pendiente";
    }

    public void AgregarItem(Producto producto, int cantidad)
    {
        producto.ReducirStock(cantidad);
        _items.Add(new OrdenItem(producto.Id, cantidad, producto.Precio));
        MontoTotal += producto.Precio * cantidad;
    }

    public void MarcarComoPagada(string transactionId)
    {
        Estado = "Aprobada";
        TransactionId = transactionId;
    }

    public void MarcarComoRechazada()
    {
        Estado = "Rechazada";
    }

    private Orden() { }
}