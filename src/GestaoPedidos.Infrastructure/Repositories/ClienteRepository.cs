using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly GestaoPedidosDbContext _context;

    public ClienteRepository(GestaoPedidosDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Cliente?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Cliente>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        await _context.Clientes.AddAsync(cliente, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
