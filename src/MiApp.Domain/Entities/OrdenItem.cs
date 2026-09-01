namespace MiApp.Domain.Entities;

public class OrdenItem
{
    public int Id { get; private set; }
    public int OrdenId { get; private set; }
    public int ProductoId { get; private set; }
    public Producto Producto { get; private set; } = null!;
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; private set; }

    public OrdenItem(int productoId, int cantidad, decimal precioUnitario)
    {
        ProductoId = productoId;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }

    private OrdenItem() { }
}