namespace Trabalho1.Exceptions;

public class EntregaExcecao : Exception
{
    public EntregaExcecao()
    { }

    public EntregaExcecao(string mensagem)
        : base(mensagem)
    { }

    public EntregaExcecao(string mensagem, Exception innerException)
        : base(mensagem, innerException)
    { }
}