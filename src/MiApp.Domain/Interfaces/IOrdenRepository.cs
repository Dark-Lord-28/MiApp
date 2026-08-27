namespace MiApp.Domain.Interfaces;

using MiApp.Domain.Entities;

public interface IOrdenRepository
{
    Task AddAsync(Orden orden);
    Task<Orden?> GetByIdAsync(int id);
    Task UpdateAsync(Orden orden);
}