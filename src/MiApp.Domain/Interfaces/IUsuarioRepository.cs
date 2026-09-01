using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task AddAsync(Usuario usuario);
    void Update(Usuario usuario);
    void Delete(Usuario usuario);
    Task SaveChangesAsync();
}