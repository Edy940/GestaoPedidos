namespace GestaoPedidos.Domain.Common;

public abstract class Entidade
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTime DataCriacao { get; protected set; } = DateTime.UtcNow;

    public DateTime? DataAtualizacao { get; protected set; }

    public bool Excluido { get; protected set; }

    protected void Tocar()
    {
        DataAtualizacao = DateTime.UtcNow;
    }

    public void MarcarComoExcluido()
    {
        if (Excluido)
            return;

        Excluido = true;
        Tocar();
    }
}
