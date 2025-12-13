using GestaoPedidos.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace GestaoPedidos.Domain.Tests.Pedidos;

public class PedidoTests
{
    [Fact]
    public void Deve_calcular_total_do_pedido()
    {
        var pedido = new Pedido(Guid.NewGuid());

        var item = new ItemPedido(
            Guid.NewGuid(),
            "Pastel",
            10m,
            2);

        pedido.AdicionarItem(item);

        pedido.ValorTotal.Should().Be(20m);
    }
}
