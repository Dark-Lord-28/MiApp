using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface IOrdenRepository
{
    Task<Orden?> GetByIdAsync(int id);
    Task AddAsync(Orden orden);
    Task SaveChangesAsync();
}