using Microsoft.AspNetCore.Http;

namespace GestaoPedidos.Api.Contracts.Produtos;

public class CriarProdutoForm
{
    public string Nome { get; set; } = default!;
    public decimal Preco { get; set; }
    public int TipoProduto { get; set; }
    public IFormFile Foto { get; set; } = default!;
}
