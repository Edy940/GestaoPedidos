using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly GestaoPedidosDbContext _context;

    public ProdutoRepository(GestaoPedidosDbContext context)
    {
        _context = context;
    }

    public async Task<Produto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Produto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Produtos
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        await _context.Produtos.AddAsync(produto, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
