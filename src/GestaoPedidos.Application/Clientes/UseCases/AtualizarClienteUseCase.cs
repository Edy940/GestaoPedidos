using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Clientes.UseCases;

public class AtualizarClienteUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public AtualizarClienteUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository
            ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    public async Task ExecutarAsync(
        AtualizarClienteRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var cliente = await _clienteRepository.ObterPorIdAsync(request.Id, cancellationToken);

        if (cliente is null || cliente.Excluido)
            throw new KeyNotFoundException("Cliente não encontrado.");

        cliente.AtualizarDadosBasicos(
            request.Nome,
            request.Telefone,
            request.DataNascimento);

        await _clienteRepository.AtualizarAsync(cliente, cancellationToken);
    }
}
  