namespace GestaoPedidos.Application.Clientes.UseCases;

public class AtualizarClienteRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public DateTime DataNascimento { get; set; }
}
