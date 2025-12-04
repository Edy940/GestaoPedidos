using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class ObterProdutoPorIdUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public ObterProdutoPorIdUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository
            ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public async Task<Produto> ExecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var produto = await _produtoRepository.ObterPorIdAsync(id, cancellationToken);

        if (produto is null || produto.Excluido)
            throw new KeyNotFoundException("Produto não encontrado.");

        return produto;
    }
}
