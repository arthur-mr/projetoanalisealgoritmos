using Trabalho1.Modelos.Base;

namespace Trabalho1.Modelos;

public class Livro : EntidadeBase
{
    public Guid PedidoId { get; private set; }
    public string Nome { get; private set; }
    public decimal Valor {  get; private set; }
    public decimal PesoEmKg { get; private set; }

    public Livro(
        Guid pedidoId,
        string nome,
        decimal valor,
        decimal pesoEmKg)
    {
        PedidoId = pedidoId;
        Nome = nome;
        Valor = valor;
        PesoEmKg = pesoEmKg;
    }
}
