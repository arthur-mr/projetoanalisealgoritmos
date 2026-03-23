namespace B3.Dominio.Excecoes;

public class AcaoExcecao : Exception
{
    public AcaoExcecao(string mensagem)
        : base(mensagem)
    { }
}
