using GestaoPedidos.Domain.Common;
using GestaoPedidos.Domain.Enums;

namespace GestaoPedidos.Domain.Entities;

public class Produto : Entidade
{
    public string Nome { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public string FotoKey { get; private set; } = null!;
    public TipoProduto TipoProduto { get; private set; }

    private Produto() { }

    public Produto(string nome, decimal preco, string fotoKey, TipoProduto tipoProduto)
    {
        DefinirNome(nome);
        DefinirPreco(preco);
        DefinirFotoKey(fotoKey);
        DefinirTipoProduto(tipoProduto);
    }

    public void Atualizar(string nome, decimal preco, string fotoKey, TipoProduto tipoProduto)
    {
        DefinirNome(nome);
        DefinirPreco(preco);
        DefinirFotoKey(fotoKey);
        DefinirTipoProduto(tipoProduto);
        Tocar();
    }

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

    private void DefinirPreco(decimal preco)
    {
        if (preco <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.", nameof(Preco));

        Preco = preco;
    }

 
















   private void DefinirFotoKey(string fotoKey)
    {
        GarantirNaoVazio(fotoKey, nameof(FotoKey));
        FotoKey = fotoKey.Trim();
    }

    private void DefinirTipoProduto(TipoProduto tipoProduto)
    {
        if (tipoProduto == TipoProduto.Desconhecido)
            throw new ArgumentException("O tipo de produto é obrigatório.", nameof(TipoProduto));

 










       TipoProduto = tipoProduto;
    }
}
