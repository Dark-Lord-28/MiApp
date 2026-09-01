using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;

namespace MiApp.Infrastructure.Repositories;

public class OrdenRepository : IOrdenRepository
{
    private readonly AppDbContext _context;

    public OrdenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Orden?> GetByIdAsync(int id)
    {
        return await _context.Ordenes
            .Include(o => o.Items)
                .ThenInclude(i => i.Producto)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Orden orden)
    {
        await _context.Ordenes.AddAsync(orden);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}