using B3.Dominio.Modelos.Base;

namespace B3.Dominio.Modelos;

public class AcaoInvestidor : ModeloComExclusao
{
    public Guid AcaoId { get; set; }
    public Guid InvestidorId { get; set; }
    public virtual Investidor Investidor { get; set; }

    protected AcaoInvestidor()
    { }

    public AcaoInvestidor(Guid acaoId, Guid investidorId)
    {
        AcaoId = acaoId;
        InvestidorId = investidorId;
    }
}