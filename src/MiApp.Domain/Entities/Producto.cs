namespace MiApp.Domain.Entities;

public class Producto
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public decimal Precio { get; private set; }
    public int Stock { get; private set; }

    public Producto(string nombre, decimal precio, int stock)
    {
        if (precio <= 0) throw new ArgumentException("El precio debe ser mayor a 0.");
        if (stock < 0) throw new ArgumentException("El stock no puede ser negativo.");

        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }

    public void ReducirStock(int cantidad)
    {
        if (cantidad > Stock) throw new InvalidOperationException($"Stock insuficiente para el producto '{Nombre}'.");
        Stock -= cantidad;
    }

    private Producto() { }
}