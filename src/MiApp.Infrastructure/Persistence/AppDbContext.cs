using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<OrdenItem> OrdenItems => Set<OrdenItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasMany(o => o.Items)
                  .WithOne()
                  .HasForeignKey(i => i.OrdenId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}