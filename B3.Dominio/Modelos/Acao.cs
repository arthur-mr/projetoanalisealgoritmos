using B3.Dominio.Modelos.Base;

namespace B3.Dominio.Modelos;

public class Acao : ModeloComExclusao
{
    public string Nome { get; set; }
    public decimal ValorAtual { get; set; }
    public IList<AcaoInvestidor> InvestidoresInteressados { get; set; }

    protected Acao()
    { 
        InvestidoresInteressados = new List<AcaoInvestidor>();
    }
}