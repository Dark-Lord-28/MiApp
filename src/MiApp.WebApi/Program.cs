using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiApp.Application.Interfaces;
using MiApp.Application.Services;
using MiApp.Application.Orders.Commands;
using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;
using MiApp.Infrastructure.Repositories;
using MiApp.Infrastructure.Services;
using MiApp.Infrastructure.Clients;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de Inyección de Dependencias
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Registrar MediatR escaneando el ensamblado de Application
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CrearOrdenCommand).Assembly));

// Configurar Autenticación JWT
var jwtKey = builder.Configuration["JwtSettings:SecretKey"] 
    ?? throw new InvalidOperationException("La clave secreta 'JwtSettings:SecretKey' no está configurada.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar Swagger con soporte JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiApp API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresá el token obtenido en el endpoint de login."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Registrar Typed HttpClient para PaymentService
builder.Services.AddHttpClient<IPaymentClient, PaymentClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:Payment"] ?? "https://localhost:5001");
    c.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

// Aplicar migraciones y sembrado de datos (Seeding) al iniciar la API
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    await context.Database.MigrateAsync();

    // Seeding de Administrador inicial
    if (!await context.Usuarios.AnyAsync(u => u.Rol == "Admin"))
    {
        var passwordHashAdmin = hasher.Hash("Admin1234!");
        context.Usuarios.Add(new Usuario("Admin Principal", "admin@miapp.com", passwordHashAdmin, "Admin"));
        await context.SaveChangesAsync();
    }

    // Seeding de Productos iniciales
    if (!await context.Productos.AnyAsync())
    {
        context.Productos.AddRange(
            new Producto("Teclado Mecánico", 150000m, 100),
            new Producto("Mouse Gamer", 8000m, 250),
            new Producto("Monitor 24 Pulgadas", 45000m, 5)
        );
        await context.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();