using GestaoPedidos.Domain.Common;

namespace GestaoPedidos.Domain.Entities;

public class Pedido : Entidade
{
    public Guid ClienteId { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public decimal ValorTotal { get; private set; }

    // Navegação
    public Cliente? Cliente { get; private set; }
    private readonly List<ItemPedido> _itens = new();
    public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

    private Pedido()
    {
    }

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

    public void RemoverItem(Guid itemId)
    {
        var item = _itens.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
            return;

        _itens.Remove(item);
        RecalcularValorTotal();
        Tocar();
    }

    public void AtualizarQuantidadeItem(Guid itemId, int novaQuantidade)
    {
        var item = _itens.FirstOrDefault(i => i.Id == itemId);
        if (item is null)
            throw new InvalidOperationException("Item do pedido não encontrado.");

        item.AtualizarQuantidade(novaQuantidade);
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
