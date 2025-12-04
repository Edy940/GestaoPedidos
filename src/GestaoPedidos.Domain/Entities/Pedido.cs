using GestaoPedidos.Domain.Common;

namespace GestaoPedidos.Domain.Entities;

public class Pedido : Entidade
{
    public Guid ClienteId { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public decimal ValorTotal { get; private set; }

    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    private Pedido() { }

    public Pedido(Guid clienteId)
    {
        if (clienteId == Guid.Empty)
            throw new ArgumentException("Cliente inválido.", nameof(ClienteId));

        ClienteId = clienteId;
        DataCriacao = DateTime.UtcNow;
        ValorTotal = 0;
    }

    public void AdicionarItem(ItemPedido item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _itens.Add(item);
        RecalcularValorTotal();
        Tocar();
    }



















    public void ValidarSePossuiItens()
    {
        if (!_itens.Any())
            throw new InvalidOperationException("O pedido deve conter pelo menos um produto.");
    }

    private void RecalcularValorTotal()
    {
        ValorTotal = _itens.Sum(i => i.ValorTotalItem);
    }
}
