using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Persistence.Configurations;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.NomeProduto)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.PrecoUnitario)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Quantidade)
            .IsRequired();

        builder.Property(i => i.ValorTotalItem)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.DataCriacao)
            .IsRequired();

        builder.Property(i => i.Excluido)
            .HasDefaultValue(false);
    }
}
