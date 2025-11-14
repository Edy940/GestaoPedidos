using GestaoPedidos.Application.Interfaces.Repositories;
using GestaoPedidos.Domain.Entities;

namespace GestaoPedidos.Application.Clientes.UseCases;

public class CriarClienteUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public CriarClienteUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository
            ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    public async Task<CriarClienteResponse> ExecutarAsync(
        CriarClienteRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        // Regra: não podem existir dois clientes com o mesmo email
        var existente = await _clienteRepository.ObterPorEmailAsync(
            request.Email, cancellationToken);

        if (existente is not null && !existente.Excluido)
            throw new InvalidOperationException("Já existe um cliente cadastrado com este e-mail.");

        var cliente = new Cliente(
            request.Nome,
            request.Email,
            request.Telefone,
            request.DataNascimento,
            request.Endereco,
            request.Complemento,
            request.Bairro,
            request.Cep);

        await _clienteRepository.AdicionarAsync(cliente, cancellationToken);

        return new CriarClienteResponse
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            DataCadastro = cliente.DataCadastro
        };
    }
}
