using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ClienteId)
            .IsRequired();

        builder.Property(p => p.DataCriacao)
            .IsRequired();

        builder.Property(p => p.ValorTotal)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Excluido)
            .HasDefaultValue(false);

        builder.HasMany(p => p.Itens)
            .WithOne()
            .HasForeignKey(i => i.PedidoId);
    }
}
