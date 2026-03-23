using B3.Dominio.Enumeradores;
using B3.Dominio.Modelos.Base;

namespace B3.Dominio.Modelos;

public class Ordem : ModeloComExclusao
{
    public Guid InvestidorId { get; private set; }
    public string NomeInvestidor => Investidor?.Nome;
    public virtual Investidor Investidor { get; private set; }
    public Guid AcaoId { get; private set; }
    public virtual Acao Acao { get; private set; }
    public TipoOrdem Tipo { get; private set; }
    public StatusOrdem Status { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataFinalizacao { get; private set; }
    public decimal Valor { get; private set; }

    protected Ordem()
    { }

    public Ordem(
        Guid investidorId,
        Guid acaoId,
        TipoOrdem tipo,
        decimal valor)
    {
        InvestidorId = investidorId;
        AcaoId = acaoId;
        Tipo = tipo;
        Valor = valor;
        Status = StatusOrdem.Pendente;
        DataCriacao = DateTime.Now;
    }

    public void Finalizar()
    {
        Status = StatusOrdem.Finalizada;
        DataFinalizacao = DateTime.Now;
    }
}