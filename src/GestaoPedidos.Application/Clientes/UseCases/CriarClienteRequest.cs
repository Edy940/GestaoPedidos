namespace GestaoPedidos.Application.Clientes.UseCases;

public class CriarClienteRequest
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; } = null!;
    public string? Complemento { get; set; }
    public string Bairro { get; set; } = null!;
    public string Cep { get; set; } = null!;
}
