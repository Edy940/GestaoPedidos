using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly GestaoPedidosDbContext _context;

    public PedidoRepository(GestaoPedidosDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Pedido>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        await _context.Pedidos.AddAsync(pedido, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Pedido pedido, CancellationToken cancellationToken = default)
    {
        if (pedido == null)
            throw new ArgumentNullException(nameof(pedido));

        pedido.Ativo = false; // Implementação de soft delete
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync(cancellationToken);
    }
}