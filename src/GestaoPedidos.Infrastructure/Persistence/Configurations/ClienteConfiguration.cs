using GestaoPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoPedidos.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.Telefone)
            .HasMaxLength(20);

        builder.Property(c => c.Endereco)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(c => c.Bairro)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Cep)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.DataCriacao)
            .IsRequired();

        builder.Property(c => c.Excluido)
            .HasDefaultValue(false);
    }
}
