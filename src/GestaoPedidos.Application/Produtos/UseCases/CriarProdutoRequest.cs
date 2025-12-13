using GestaoPedidos.Domain.Enums;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class CriarProdutoRequest
{
    public string Nome { get; set; } = null!;
    public decimal Preco { get; set; }
    public string FotoKey { get; set; } = null!;
    public TipoProduto TipoProduto { get; set; }
}
