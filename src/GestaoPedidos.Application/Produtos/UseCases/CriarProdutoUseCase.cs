using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Application.Interfaces.Storage;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Enums;

namespace GestaoPedidos.Application.Produtos.UseCases;

public class CriarProdutoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProductImageStorageService _imageStorage;

    public CriarProdutoUseCase(
        IProdutoRepository produtoRepository,
        IProductImageStorageService imageStorage)
    {
        _produtoRepository = produtoRepository
            ?? throw new ArgumentNullException(nameof(produtoRepository));

        _imageStorage = imageStorage
            ?? throw new ArgumentNullException(nameof(imageStorage));
    }

    public async Task<CriarProdutoResponse> ExecutarAsync(
        CriarProdutoRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        // 2) Cria a entidade de domínio já com a FotoKey vinda do S3
        var produto = new Produto(
          request.Nome,
          request.Preco,
          request.FotoKey, // Corrigido: Substituído request.FotoKey por fotoKey
          (TipoProduto)request.TipoProduto);

        // 3) Persiste no banco
        await _produtoRepository.AdicionarAsync(produto, cancellationToken);

        // 4) Devolve o DTO de resposta
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
