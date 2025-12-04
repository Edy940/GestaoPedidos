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

    public ProdutosController(
        CriarProdutoUseCase criarProduto,
        AtualizarProdutoUseCase atualizarProduto,
        ListarProdutosUseCase listarProdutos,
        ObterProdutoPorIdUseCase obterProdutoPorId,
        RemoverProdutoUseCase removerProduto)
    {
        _criarProduto = criarProduto;
        _atualizarProduto = atualizarProduto;
        _listarProdutos = listarProdutos;
        _obterProdutoPorId = obterProdutoPorId;
        _removerProduto = removerProduto;
    }

    // POST api/produtos
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
