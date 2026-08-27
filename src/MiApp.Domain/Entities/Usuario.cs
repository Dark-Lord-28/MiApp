namespace MiApp.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Rol { get; private set; } = "User";
    public DateTime FechaCreacion { get; private set; }

    // Constructor privado requerido por EF Core
    private Usuario() { }

    // Constructor de Dominio
    public Usuario(string nombre, string email, string password, string rol = "User")
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email no puede estar vacío.", nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));

        Nombre = nombre;
        Email = email;
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        Rol = string.IsNullOrWhiteSpace(rol) ? "User" : rol;
        FechaCreacion = DateTime.UtcNow;
    }

    public void ActualizarNombre(string nuevoNombre)
    {
        if (string.IsNullOrWhiteSpace(nuevoNombre))
            throw new ArgumentException("El nuevo nombre no puede estar vacío.", nameof(nuevoNombre));

        Nombre = nuevoNombre;
    }

    public bool ValidarPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}