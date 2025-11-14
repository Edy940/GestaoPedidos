using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Pedidos.UseCases;

public class ObterPedidoPorIdUseCase
{
    private readonly IPedidoRepository _pedidoRepository;

    public ObterPedidoPorIdUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<Pedido> ExecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var pedido = await _pedidoRepository.ObterPorIdAsync(id, cancellationToken);

        if (pedido is null || pedido.Excluido)
            throw new KeyNotFoundException("Pedido não encontrado.");

        return pedido;
    }
}
