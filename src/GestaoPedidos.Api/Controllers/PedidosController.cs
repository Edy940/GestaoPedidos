using GestaoPedidos.Application.Pedidos.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PedidosController : ControllerBase
{
    private readonly CriarPedidoUseCase _criar;
    private readonly ListarPedidosUseCase _listar;
    private readonly ObterPedidoPorIdUseCase _obter;
    private readonly RemoverPedidoUseCase _remover;

    public PedidosController(
        CriarPedidoUseCase criar,
        ListarPedidosUseCase listar,
        ObterPedidoPorIdUseCase obter,
        RemoverPedidoUseCase remover)
    {
        _criar = criar;
        _listar = listar;
        _obter = obter;
        _remover = remover;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest request, CancellationToken cancellationToken)
    {
        var pedido = await _criar.ExecutarAsync(request, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = pedido.PedidoId }, pedido);
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var pedidos = await _listar.ExecutarAsync(cancellationToken);
        return Ok(pedidos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var pedido = await _obter.ExecutarAsync(id, cancellationToken);
        return Ok(pedido);
    }

    // 🔹 Por enquanto, o PUT vai só responder "não implementado"
    //   assim você CONSEGUE buildar e testar GET sem travar no Update
    [HttpPut("{id:guid}")]
    public Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarPedidoRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IActionResult>(
            StatusCode(StatusCodes.Status501NotImplemented, "AtualizarPedido ainda não foi implementado."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        await _remover.ExecutarAsync(id, cancellationToken);
        return NoContent();
    }
}