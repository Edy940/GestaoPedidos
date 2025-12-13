using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Pedidos.UseCases;

public class CriarPedidoUseCase
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public CriarPedidoUseCase(
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository,
        IPedidoRepository pedidoRepository)
    {
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<CriarPedidoResponse> ExecutarAsync(
        CriarPedidoRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Itens == null || !request.Itens.Any())
            throw new InvalidOperationException("O pedido deve conter ao menos um item.");

        // 1) Validar cliente
        var cliente = await _clienteRepository.ObterPorIdAsync(request.ClienteId, cancellationToken);
        if (cliente is null || cliente.Excluido)
            throw new KeyNotFoundException("Cliente não encontrado.");

        // 2) Criar pedido
        var pedido = new Pedido(cliente.Id);

        // 3) Validar cada produto + criar item
        foreach (var itemReq in request.Itens)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(itemReq.ProdutoId, cancellationToken);
            if (produto is null || produto.Excluido)
                throw new KeyNotFoundException($"Produto {itemReq.ProdutoId} não encontrado.");

            if (itemReq.Quantidade <= 0)
                throw new InvalidOperationException("Quantidade deve ser maior que zero.");

            var item = new ItemPedido(
                produto.Id,
                produto.Nome,
                produto.Preco,
                itemReq.Quantidade
            );

            pedido.AdicionarItem(item);
        }

        pedido.ValidarSePossuiItens();

        // 4) Registrar no repositório
        await _pedidoRepository.AdicionarAsync(pedido, cancellationToken);

        // 📌 5) Ponto de extensão: envio de evento SNS/SQS (implementaremos depois)
        // Exemplo:
        // await _pedidoEventPublisher.PublicarPedidoCriado(pedido);

        return new CriarPedidoResponse
        {
            PedidoId = pedido.Id,
            ClienteId = pedido.ClienteId,
            ValorTotal = pedido.ValorTotal,
            DataCriacao = pedido.DataCriacao,
        };
    }
}
