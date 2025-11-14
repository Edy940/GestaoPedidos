using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Cliente?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Cliente>> ListarAsync(CancellationToken cancellationToken = default);

    Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default);
    Task AtualizarAsync(Cliente cliente, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete: marca o cliente como excluído, não remove fisicamente.
    /// </summary>
    Task RemoverAsync(Cliente cliente, CancellationToken cancellationToken = default);
}
