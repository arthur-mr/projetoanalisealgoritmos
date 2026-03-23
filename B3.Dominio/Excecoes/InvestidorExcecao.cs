namespace B3.Dominio.Excecoes;

public class InvestidorExcecao : Exception
{
    public InvestidorExcecao(string mensagem)
        : base(mensagem)
    { }
}