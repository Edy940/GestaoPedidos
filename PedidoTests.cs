using GestaoPedidos.Domain.Entities;
using FluentAssertions;

namespace GestaoPedidos.Domain.Tests.Pedidos;

public class PedidoTests
{
    [Fact]
    public void Deve_adicionar_itens_e_calcular_valor_total_do_pedido()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var pedido = new Pedido(clienteId);

        var item1 = new ItemPedido(
            produtoId: Guid.NewGuid(),
            nomeProduto: "Pastel de Carne",
            precoUnitario: 10m,
            quantidade: 2);

        var item2 = new ItemPedido(
            produtoId: Guid.NewGuid(),
            nomeProduto: "Refrigerante",
            precoUnitario: 5m,
            quantidade: 1);

        // Act
        pedido.AdicionarItem(item1);
        pedido.AdicionarItem(item2);

        // Assert
        pedido.Itens.Should().HaveCount(2);
        pedido.ValorTotal.Should().Be(25m); // 2 * 10 + 1 * 5
    }

    [Fact]
    public void Nao_deve_permitir_pedido_sem_itens()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var pedido = new Pedido(clienteId);

        // Act
        var acao = () => pedido.ValidarSePossuiItens();

        // Assert
        acao.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*deve conter pelo menos um produto*");
    }

    [Fact]
    public void Deve_atualizar_quantidade_item_e_recalcular_total()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var pedido = new Pedido(clienteId);

        var item = new ItemPedido(
            produtoId: Guid.NewGuid(),
            nomeProduto: "Pastel de Queijo",
            precoUnitario: 8m,
            quantidade: 1);

        pedido.AdicionarItem(item);

        var itemId = item.Id;

        // Act
        pedido.AtualizarQuantidadeItem(itemId, 3);

        // Assert
        pedido.ValorTotal.Should().Be(24m); // 3 * 8
        pedido.Itens.First(i => i.Id == itemId).Quantidade.Should().Be(3);
    }
}
