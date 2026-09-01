using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;

namespace MiApp.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }

    public void Update(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
    }

    public void Delete(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}