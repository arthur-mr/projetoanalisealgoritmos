namespace B3.Dominio.Interfaces;

public interface IPublicadorMensagemServico
{
    void Publicar<TMensagem>(TMensagem mensagem);
}