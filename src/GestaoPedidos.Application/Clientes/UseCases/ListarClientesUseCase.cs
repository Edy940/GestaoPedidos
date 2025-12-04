using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Clientes.UseCases;

public class ListarClientesUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public ListarClientesUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository
            ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    public async Task<IReadOnlyCollection<Cliente>> ExecutarAsync(
        CancellationToken cancellationToken = default)
    {
        return await _clienteRepository.ListarAsync(cancellationToken);
    }
}
