namespace B3.Dominio.Interfaces;

public interface IEmailServico
{
    void EnviarEmail(string destinatario, string assunto, string mensagem);
}