using GestaoPedidos.Domain.Common;

namespace GestaoPedidos.Domain.Entities;

public class Cliente : Entidade
{
    public string Nome { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Telefone { get; private set; } = null!;
    public DateTime DataNascimento { get; private set; }
    public string Endereco { get; private set; } = null!;
    public string? Complemento { get; private set; }
    public string Bairro { get; private set; } = null!;
    public string Cep { get; private set; } = null!;
    public DateTime DataCadastro { get; private set; }

    // Construtor vazio para ORM
    private Cliente()
    {
    }

    public Cliente(
        string nome,
        string email,
        string telefone,
        DateTime dataNascimento,
        string endereco,
        string? complemento,
        string bairro,
        string cep)
    {
        DefinirNome(nome);
        DefinirEmail(email);
        DefinirTelefone(telefone);
        DefinirDataNascimento(dataNascimento);
        DefinirEndereco(endereco);
        DefinirComplemento(complemento);
        DefinirBairro(bairro);
        DefinirCep(cep);

        DataCadastro = DateTime.UtcNow;
    }

    #region Métodos de atualização

    public void AtualizarDadosBasicos(
        string nome,
        string telefone,
        DateTime dataNascimento)
    {
        DefinirNome(nome);
        DefinirTelefone(telefone);
        DefinirDataNascimento(dataNascimento);
        Tocar();
    }

    public void AtualizarEndereco(
        string endereco,
        string? complemento,
        string bairro,
        string cep)
    {
        DefinirEndereco(endereco);
        DefinirComplemento(complemento);
        DefinirBairro(bairro);
        DefinirCep(cep);
        Tocar();
    }

    #endregion

    #region Validações internas

    private static void GarantirNaoVazio(string? valor, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"O campo {nomeCampo} é obrigatório.", nomeCampo);
    }

    private void DefinirNome(string nome)
    {
        GarantirNaoVazio(nome, nameof(Nome));
        Nome = nome.Trim();
    }

    private void DefinirEmail(string email)
    {
        GarantirNaoVazio(email, nameof(Email));

        if (!email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("O e-mail informado é inválido.", nameof(Email));

        Email = email.Trim().ToLowerInvariant();
    }

    private void DefinirTelefone(string telefone)
    {
        GarantirNaoVazio(telefone, nameof(Telefone));
        Telefone = telefone.Trim();
    }

    private void DefinirDataNascimento(DateTime dataNascimento)
    {
        if (dataNascimento == default)
            throw new ArgumentException("A data de nascimento é obrigatória.", nameof(DataNascimento));

        if (dataNascimento > DateTime.UtcNow)
            throw new ArgumentException("A data de nascimento não pode estar no futuro.", nameof(DataNascimento));

        DataNascimento = dataNascimento;
    }

    private void DefinirEndereco(string endereco)
    {
        GarantirNaoVazio(endereco, nameof(Endereco));
        Endereco = endereco.Trim();
    }

    private void DefinirComplemento(string? complemento)
    {
        Complemento = string.IsNullOrWhiteSpace(complemento)
            ? null
            : complemento.Trim();
    }

    private void DefinirBairro(string bairro)
    {
        GarantirNaoVazio(bairro, nameof(Bairro));
        Bairro = bairro.Trim();
    }

    private void DefinirCep(string cep)
    {
        GarantirNaoVazio(cep, nameof(Cep));
        Cep = cep.Trim();
    }

    #endregion
}
