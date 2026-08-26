namespace MiApp.Domain.Interfaces;

using MiApp.Domain.Entities;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task AddAsync(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(Usuario usuario);
    Task SaveChangesAsync();
}