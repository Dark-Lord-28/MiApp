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
        //Aqui se prepara todo para guardar en la base de datos
        await _context.SaveChangesAsync();
    }

    public async Task<Orden?> GetByIdAsync(int id)
    {
        return await _context.Set<Orden>().FindAsync(id);
    }

    public async Task UpdateAsync(Orden orden)
    {
        _context.Set<Orden>().Update(orden);
        //Aqui se prepara todo para guardar en la base de datos
        await _context.SaveChangesAsync();
    }
}