using GestaoPedidos.Domain.Common;

namespace GestaoPedidos.Domain.Entities;

public class ItemPedido : Entidade
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string NomeProduto { get; private set; } = null!;
    public decimal PrecoUnitario { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorTotalItem { get; private set; }

    private ItemPedido() { }

    public ItemPedido(Guid produtoId, string nomeProduto, decimal precoUnitario, int quantidade)
    {
        DefinirProduto(produtoId, nomeProduto, precoUnitario);
        DefinirQuantidade(quantidade);
        CalcularTotal();
    }

    public void AtualizarQuantidade(int novaQuantidade)
    {
        DefinirQuantidade(novaQuantidade);
 








       CalcularTotal();
        Tocar();
    }

    private void DefinirProduto(Guid produtoId, string nomeProduto, decimal precoUnitario)
    {
        if (produtoId == Guid.Empty)
            throw new ArgumentException("Produto inválido.", nameof(ProdutoId));

        if (string.IsNullOrWhiteSpace(nomeProduto))
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(NomeProduto));

 











       if (precoUnitario <= 0)
            throw new ArgumentException("O preço unitário deve ser maior que zero.", nameof(PrecoUnitario));

        ProdutoId = produtoId;
        NomeProduto = nomeProduto.Trim();
        PrecoUnitario = precoUnitario;
    }

    private void DefinirQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(Quantidade));













        Quantidade = quantidade;
    }

    private void CalcularTotal()
    {
        ValorTotalItem = PrecoUnitario * Quantidade;
    }
}








