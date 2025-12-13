using GestaoPedidos.Domain.Entities;
using FluentAssertions;

namespace GestaoPedidos.Domain.Tests.Clientes;

public class ClienteTests
{
    [Fact]
    public void Deve_criar_cliente_valido_quando_dados_fore_validos()
    {
        // Arrange
        var nome = "João Silva";
        var email = "joao.silva@example.com";
        var telefone = "11999999999";
        var dataNascimento = new DateTime(1990, 1, 1);
        var endereco = "Rua A, 123";
        string? complemento = "Apto 10";
        var bairro = "Centro";
        var cep = "01000-000";

        // Act
        var cliente = new Cliente(
            nome,
            email,
            telefone,
            dataNascimento,
            endereco,
            complemento,
            bairro,
            cep);

        // Assert
        cliente.Nome.Should().Be(nome);
        cliente.Email.Should().Be(email.ToLowerInvariant());
        cliente.Telefone.Should().Be(telefone);
        cliente.DataNascimento.Should().Be(dataNascimento);
        cliente.Endereco.Should().Be(endereco);
        cliente.Complemento.Should().Be(complemento);
        cliente.Bairro.Should().Be(bairro);
        cliente.Cep.Should().Be(cep);
        cliente.Excluido.Should().BeFalse();
        cliente.DataCadastro.Should().NotBe(default);
    }

    [Fact]
    public void Nao_deve_permitir_criar_cliente_com_email_invalido()
    {
        // Arrange
        var nome = "João Silva";
        var email = "email_invalido";
        var telefone = "11999999999";
        var dataNascimento = new DateTime(1990, 1, 1);
        var endereco = "Rua A, 123";
        string? complemento = "Apto 10";
        var bairro = "Centro";
        var cep = "01000-000";

        // Act
        var acao = () => new Cliente(
            nome,
            email,
            telefone,
            dataNascimento,
            endereco,
            complemento,
            bairro,
            cep);

        // Assert
        acao.Should()
            .Throw<ArgumentException>()
            .WithMessage("*e-mail informado é inválido*");
    }

    [Fact]
    public void Deve_marcar_cliente_como_excluido_quando_soft_delete_for_chamado()
    {
        // Arrange
        var cliente = new Cliente(
            "João Silva",
            "joao.silva@example.com",
            "11999999999",
            new DateTime(1990, 1, 1),
            "Rua A, 123",
            null,
            "Centro",
            "01000-000");

        // Act
        cliente.MarcarComoExcluido();

        // Assert
        cliente.Excluido.Should().BeTrue();
        cliente.DataAtualizacao.Should().NotBeNull();
    }
}
