using GestaoPedidos.Application.Clientes.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPedidos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly CriarClienteUseCase _criarCliente;
    private readonly AtualizarClienteUseCase _atualizarCliente;
    private readonly ListarClientesUseCase _listarClientes;
    private readonly ObterClientePorIdUseCase _obterClientePorId;
    private readonly RemoverClienteUseCase _removerCliente;

    public ClientesController(
        CriarClienteUseCase criarCliente,
        AtualizarClienteUseCase atualizarCliente,
        ListarClientesUseCase listarClientes,
        ObterClientePorIdUseCase obterClientePorId,
        RemoverClienteUseCase removerCliente)
    {
        _criarCliente = criarCliente;
        _atualizarCliente = atualizarCliente;
        _listarClientes = listarClientes;
        _obterClientePorId = obterClientePorId;
        _removerCliente = removerCliente;
    }

    // POST api/clientes
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _criarCliente.ExecutarAsync(request, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
    }

    // GET api/clientes
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var clientes = await _listarClientes.ExecutarAsync(cancellationToken);
        return Ok(clientes);
    }

    // GET api/clientes/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var cliente = await _obterClientePorId.ExecutarAsync(id, cancellationToken);
        return Ok(cliente);
    }

    // PUT api/clientes/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarClienteRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest("O ID da rota é diferente do ID do corpo da requisição.");

        await _atualizarCliente.ExecutarAsync(request, cancellationToken);
        return NoContent();
    }

    // DELETE api/clientes/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id, CancellationToken cancellationToken)
    {
        await _removerCliente.ExecutarAsync(id, cancellationToken);
        return NoContent();
    }
}
