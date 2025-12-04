using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Interfaces.Repositories;

public interface IProdutoRepository
{
    Task<Produto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Produto>> ListarAsync(CancellationToken cancellationToken = default);

    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Produto produto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete (marca como excluído).
    /// </summary>
    Task RemoverAsync(Produto produto, CancellationToken cancellationToken = default);
}
