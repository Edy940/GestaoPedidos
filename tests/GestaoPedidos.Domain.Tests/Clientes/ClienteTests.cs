using GestaoPedidos.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace GestaoPedidos.Domain.Tests.Clientes;

public class ClienteTests
{
    [Fact]
    public void Deve_criar_cliente_valido()
    {
        var cliente = new Cliente(
            "Joao",
            "joao@example.com",
            "11999999999",
            new DateTime(1990, 1, 1),
            "Rua X",
            null,
            "Centro",
            "01000-000");

        cliente.Nome.Should().Be("Joao");
        cliente.Email.Should().Be("joao@example.com");
        cliente.Excluido.Should().BeFalse();
    }
}
