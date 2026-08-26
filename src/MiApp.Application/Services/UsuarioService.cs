namespace MiApp.Application.Services;

using MiApp.Application.DTOs;
using MiApp.Application.Interfaces;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Email = u.Email,
            FechaCreacion = u.FechaCreacion
        });
    }

    public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            FechaCreacion = usuario.FechaCreacion
        };
    }

    public async Task<UsuarioDto> CrearAsync(CrearUsuarioDto dto)
    {
        var nuevoUsuario = new Usuario(dto.Nombre, dto.Email);
        await _repository.AddAsync(nuevoUsuario);
        await _repository.SaveChangesAsync();

        return new UsuarioDto
        {
            Id = nuevoUsuario.Id,
            Nombre = nuevoUsuario.Nombre,
            Email = nuevoUsuario.Email,
            FechaCreacion = nuevoUsuario.FechaCreacion
        };
    }

    public async Task<bool> ActualizarAsync(int id, ActualizarUsuarioDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return false;

        usuario.ActualizarNombre(dto.Nombre);
        _repository.Update(usuario);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return false;

        _repository.Delete(usuario);
        await _repository.SaveChangesAsync();
        return true;
    }
}