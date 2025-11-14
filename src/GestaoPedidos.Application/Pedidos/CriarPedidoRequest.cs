namespace GestaoPedidos.Application.Pedidos.UseCases;

public class CriarPedidoItemRequest
{
    public Guid ProdutoId { get; set; }
    public int Quantidade { get; set; }
}

public class CriarPedidoRequest
{
    public Guid ClienteId { get; set; }
    public List<CriarPedidoItemRequest> Itens { get; set; } = new();
}
