namespace MiApp.Infrastructure.Repositories;

using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;

public class OrdenRepository : IOrdenRepository
{
    private readonly AppDbContext _context;

    public OrdenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Orden orden)
    {
        await _context.Set<Orden>().AddAsync(orden);
        await _context.SaveChangesAsync();
    }

    public async Task<Orden?> GetByIdAsync(int id)
    {
        return await _context.Set<Orden>().FindAsync(id);
    }

    public async Task UpdateAsync(Orden orden)
    {
        _context.Set<Orden>().Update(orden);
        await _context.SaveChangesAsync();
    }
}