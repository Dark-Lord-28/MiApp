using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> GetAllAsync();
    Task<Producto?> GetByIdAsync(int id);
    Task AddAsync(Producto producto);
    Task SaveChangesAsync();
}