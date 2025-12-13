using GestaoPedidos.Domain.Enums;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class CriarProdutoResponse
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public decimal Preco { get; init; }
    public string FotoKey { get; init; } = null!;
    public TipoProduto TipoProduto { get; init; }
}
