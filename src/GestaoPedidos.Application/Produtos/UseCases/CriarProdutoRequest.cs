namespace GestaoPedidos.Application.Produtos.UseCases;

public class CriarProdutoRequest
{
    public string Nome { get; set; } = default!;
    public decimal Preco { get; set; }
    public int TipoProduto { get; set; }
    public string FotoKey { get; set; } = default!;
}
