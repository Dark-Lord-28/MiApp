namespace MiApp.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime FechaCreacion { get; private set; }

    // Constructor privado requerido por Entity Framework Core
    private Usuario() { }

    // Constructor público para instanciar la entidad desde la capa de Aplicación
    public Usuario(string nombre, string email)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email no puede estar vacío.", nameof(email));

        Nombre = nombre;
        Email = email;
        FechaCreacion = DateTime.UtcNow;
    }

    public void ActualizarNombre(string nuevoNombre)
    {
        if (string.IsNullOrWhiteSpace(nuevoNombre))
            throw new ArgumentException("El nuevo nombre no puede estar vacío.", nameof(nuevoNombre));

        Nombre = nuevoNombre;
    }
}