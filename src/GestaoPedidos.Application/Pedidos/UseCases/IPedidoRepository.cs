using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Interfaces.Repositories;

public interface IPedidoRepository
{
    Task<Pedido?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Pedido>> ListarAsync(CancellationToken cancellationToken = default);

    Task AdicionarAsync(Pedido pedido, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Pedido pedido, CancellationToken cancellationToken = default);
    Task RemoverAsync(Pedido pedido, CancellationToken cancellationToken = default); // soft delete
}
