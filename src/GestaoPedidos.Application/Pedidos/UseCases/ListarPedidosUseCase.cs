using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Pedidos.UseCases;

public class ListarPedidosUseCase
{
    private readonly IPedidoRepository _pedidoRepository;

    public ListarPedidosUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<IReadOnlyCollection<Pedido>> ExecutarAsync(
        CancellationToken cancellationToken = default)
    {
        return await _pedidoRepository.ListarAsync(cancellationToken);
    }
}
