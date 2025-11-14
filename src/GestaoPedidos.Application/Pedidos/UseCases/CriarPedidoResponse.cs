namespace GestaoPedidos.Application.Pedidos.UseCases;

public class CriarPedidoResponse
{
    public Guid PedidoId { get; init; }
    public Guid ClienteId { get; init; }
    public decimal ValorTotal { get; init; }
    public DateTime DataCriacao { get; init; }
}
