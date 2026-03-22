using B3.Dominio.Modelos.Base;

namespace B3.Dominio.Modelos;

public class Investidor : ModeloComExclusao
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public virtual IList<Ordem> Ordens { get; private set; }
    public virtual IList<AcaoInvestidor> AcoesInscritas { get; private set; }

    protected Investidor()
    {
        Ordens = new List<Ordem>();
        AcoesInscritas = new List<AcaoInvestidor>();
    }

    public Investidor(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}
