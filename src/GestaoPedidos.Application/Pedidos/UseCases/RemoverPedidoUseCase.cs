using GestaoPedidos.Application.Interfaces.Repositories;

namespace GestaoPedidos.Application.Pedidos.UseCases;

public class RemoverPedidoUseCase
{
    private readonly IPedidoRepository _pedidoRepository;

    public RemoverPedidoUseCase(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task ExecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var pedido = await _pedidoRepository.ObterPorIdAsync(id, cancellationToken);

        if (pedido is null || pedido.Excluido)
            throw new KeyNotFoundException("Pedido não encontrado.");

        pedido.MarcarComoExcluido();

        await _pedidoRepository.RemoverAsync(pedido, cancellationToken);
    }
}
