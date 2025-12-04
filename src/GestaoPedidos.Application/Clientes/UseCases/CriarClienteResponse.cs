namespace GestaoPedidos.Application.Clientes.UseCases;

public class CriarClienteResponse
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = null!;
    public string Email { get; init; } = null!;
    public DateTime DataCadastro { get; init; }
}
