using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class ListarProdutosUseCase
{
    private readonly IProdutoRepository _produtoRepository;

    public ListarProdutosUseCase(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository
            ?? throw new ArgumentNullException(nameof(produtoRepository));
    }

    public async Task<IReadOnlyCollection<Produto>> ExecutarAsync(
        CancellationToken cancellationToken = default)
    {
        return await _produtoRepository.ListarAsync(cancellationToken);
    }
}
