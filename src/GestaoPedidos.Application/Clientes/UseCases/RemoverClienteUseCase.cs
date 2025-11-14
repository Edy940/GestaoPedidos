using GestaoPedidos.Application.Interfaces.Repositories;

namespace GestaoPedidos.Application.Clientes.UseCases;

public class RemoverClienteUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public RemoverClienteUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository
            ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    public async Task ExecutarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var cliente = await _clienteRepository.ObterPorIdAsync(id, cancellationToken);

        if (cliente is null || cliente.Excluido)
            throw new KeyNotFoundException("Cliente não encontrado.");

        cliente.MarcarComoExcluido();

        await _clienteRepository.RemoverAsync(cliente, cancellationToken);
    }
}
