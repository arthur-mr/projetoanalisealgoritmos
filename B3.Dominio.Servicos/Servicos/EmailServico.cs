using B3.Dominio.Interfaces;
using B3.Dominio.Servicos.Modulos;
using System.Net;
using System.Net.Mail;

namespace B3.Dominio.Servicos.Servicos;

internal sealed class EmailServico : IEmailServico
{
    private readonly ConfiguracaoEmail configuracao;

    public EmailServico(ConfiguracaoEmail configuracao)
    {
        this.configuracao = configuracao;
    }

    public void EnviarEmail(
        string destinatario,
        string assunto,
        string mensagem)
    {
        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(configuracao.EmailNotificador, configuracao.TokenEmailNotificador),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        var email = new MailMessage(configuracao.EmailNotificador, destinatario)
        {
            Subject = assunto,
            Body = mensagem,
            IsBodyHtml = false
        };

        smtp.Send(email);
    }
}
