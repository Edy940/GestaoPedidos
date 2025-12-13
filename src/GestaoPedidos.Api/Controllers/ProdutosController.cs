using GestaoPedidos.Api.Contracts.Produtos;
using GestaoPedidos.Application.Interfaces.Storage;
using GestaoPedidos.Application.Produtos.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly CriarProdutoUseCase _criarProduto;
    private readonly AtualizarProdutoUseCase _atualizarProduto;
    private readonly ListarProdutosUseCase _listarProdutos;
    private readonly ObterProdutoPorIdUseCase _obterProdutoPorId;
    private readonly RemoverProdutoUseCase _removerProduto;
    private readonly IProductImageStorageService _imageStorage;


    public ProdutosController(
        CriarProdutoUseCase criarProduto,
        AtualizarProdutoUseCase atualizarProduto,
        ListarProdutosUseCase listarProdutos,
        ObterProdutoPorIdUseCase obterProdutoPorId,
        RemoverProdutoUseCase removerProduto,
        IProductImageStorageService imageStorage)
    {
        _criarProduto = criarProduto;
        _atualizarProduto = atualizarProduto;
        _listarProdutos = listarProdutos;
        _obterProdutoPorId = obterProdutoPorId;
        _removerProduto = removerProduto;
        _imageStorage = imageStorage;
    }

    // POST api/produtos
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Criar([FromForm] CriarProdutoForm form, CancellationToken cancellationToken)
    {
        if (form is null)
            return BadRequest("Form inválido.");

        if (form.Foto is null || form.Foto.Length == 0)
            return BadRequest("A foto é obrigatória.");

        if (_imageStorage is null)
            return StatusCode(500, "IProductImageStorageService não foi resolvido no DI.");

        var contentType = string.IsNullOrWhiteSpace(form.Foto.ContentType)
            ? "application/octet-stream"
            : form.Foto.ContentType;

        var fotoKey = await _imageStorage.UploadAsync(
            form.Foto.OpenReadStream(),
            form.Foto.FileName,
            contentType,
            cancellationToken);

        var request = new CriarProdutoRequest
        {
            Nome = form.Nome,
            Preco = form.Preco,
            TipoProduto = form.TipoProduto,
            FotoKey = fotoKey
        };

        var produto = await _criarProduto.ExecutarAsync(request, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
    }


    // GET api/produtos
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var produtos = await _listarProdutos.ExecutarAsync(cancellationToken);
        return Ok(produtos);
    }

    // GET api/produtos/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var produto = await _obterProdutoPorId.ExecutarAsync(id, cancellationToken);
        return Ok(produto);
    }

    // PUT api/produtos/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarProdutoRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest("O ID da rota é diferente do ID do corpo da requisição.");

        await _atualizarProduto.ExecutarAsync(request, cancellationToken);
        return NoContent();
    }

    // DELETE api/produtos/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        await _removerProduto.ExecutarAsync(id, cancellationToken);
        return NoContent();
    }
}
