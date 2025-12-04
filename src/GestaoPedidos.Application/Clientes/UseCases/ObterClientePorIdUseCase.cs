using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Clientes.UseCases;

public class ObterClientePorIdUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public ObterClientePorIdUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository
            ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    public async Task<Cliente> ExecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var cliente = await _clienteRepository.ObterPorIdAsync(id, cancellationToken);

        if (cliente is null || cliente.Excluido)
            throw new KeyNotFoundException("Cliente não encontrado.");

        return cliente;
    }
}
