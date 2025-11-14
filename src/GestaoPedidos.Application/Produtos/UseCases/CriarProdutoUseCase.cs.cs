using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class CriarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public CriarProdutoUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository
            ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public async Task<CriarProdutoResponse> ExecutarAsync(
        CriarProdutoRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var produto = new Produto(
            request.Nome,
            request.Preco,
            request.FotoKey,
            request.TipoProduto);

        await _produtoRepository.AdicionarAsync(produto, cancellationToken);

        return new CriarProdutoResponse
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            FotoKey = produto.FotoKey,
            TipoProduto = produto.TipoProduto
        };
    }
}
