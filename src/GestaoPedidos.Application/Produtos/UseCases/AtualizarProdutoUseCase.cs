using GestaoPedidos.Application.Interfaces.Repositories;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class AtualizarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public AtualizarProdutoUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository
            ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public async Task ExecutarAsync(
        AtualizarProdutoRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var produto = await _produtoRepository.ObterPorIdAsync(request.Id, cancellationToken);

        if (produto is null || produto.Excluido)
            throw new KeyNotFoundException("Produto não encontrado.");

        produto.Atualizar(
            request.Nome,
            request.Preco,
            request.FotoKey,
            request.TipoProduto);

        await _produtoRepository.AtualizarAsync(produto, cancellationToken);
    }
}
