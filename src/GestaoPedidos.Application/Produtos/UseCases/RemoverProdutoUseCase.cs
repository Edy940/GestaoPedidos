using GestaoPedidos.Application.Interfaces.Repositories;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class RemoverProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public RemoverProdutoUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository
            ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public async Task ExecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);

        if (produto is null || produto.Excluido)
            throw new KeyNotFoundException("Produto não encontrado.");

        produto.MarcarComoExcluido();

        await _produtoRepository.RemoverAsync(produto, cancellationToken);
    }
}
