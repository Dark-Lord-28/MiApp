namespace MiApp.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Rol { get; private set; } = "User";
    public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;

    public Usuario(string nombre, string email, string passwordHash, string rol = "User")
    {
        Nombre = nombre;
        Email = email;
        PasswordHash = passwordHash;
        Rol = rol;
    }
public void ActualizarDatos(string nombre, string email)
{
    if (string.IsNullOrWhiteSpace(nombre))
        throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
        
    if (string.IsNullOrWhiteSpace(email))
        throw new ArgumentException("El email no puede estar vacío.", nameof(email));

    Nombre = nombre;
    Email = email;
}
    private Usuario() { }
}