namespace B3.Dominio.Excecoes;

public class OrdemExcecao : Exception
{
    public OrdemExcecao(string mensagem)
        : base(mensagem)
    { }
}