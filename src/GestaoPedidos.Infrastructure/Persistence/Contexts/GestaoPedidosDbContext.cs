using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Persistence.Contexts;

public class GestaoPedidosDbContext : DbContext
{
    public GestaoPedidosDbContext(DbContextOptions<GestaoPedidosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoPedidosDbContext).Assembly);

        modelBuilder.Entity<Cliente>().HasQueryFilter(c => !c.Excluido);
        modelBuilder.Entity<Produto>().HasQueryFilter(p => !p.Excluido);
        modelBuilder.Entity<Pedido>().HasQueryFilter(p => !p.Excluido);
        modelBuilder.Entity<ItemPedido>().HasQueryFilter(i => !i.Excluido);

        base.OnModelCreating(modelBuilder);
    }
}
