using MediatR;
using MiApp.Application.DTOs;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;

namespace MiApp.Application.Usuarios.Commands;

public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, UsuarioDto>
{
    private readonly IUsuarioRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public CrearUsuarioCommandHandler(IUsuarioRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _passwordHasher.Hash(request.Password);
        
        
        var usuario = new Usuario(request.Nombre, request.Email, passwordHash, "User");

        await _repository.AddAsync(usuario);
        await _repository.SaveChangesAsync();

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Rol = usuario.Rol,
            FechaCreacion = usuario.FechaCreacion
        };
    }
}