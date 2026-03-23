using B3.Dominio.Excecoes;
using B3.Dominio.Interfaces;
using B3.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace B3.Dominio.Servicos.Servicos;

internal sealed class AcaoServico : IAcaoServico
{
    private readonly IEmailServico emailServico;
    private readonly IRepositorioBase repositorio;

    public AcaoServico(IEmailServico emailServico, IRepositorioBase repositorio)
    {
        this.emailServico = emailServico;
        this.repositorio = repositorio;
    }

    public async Task NotificarAlteracaoParaInvestidoresAsync(Guid acaoId, CancellationToken cancellationToken)
    {
        var acao = await repositorio.MontarConsultar<Acao>()
            .Where(x => x.Id == acaoId)
            .Include(x => x.InvestidoresInteressados)
            .ThenInclude(y => y.Investidor)
            .FirstOrDefaultAsync(cancellationToken);

        if (acao is null)
            throw new AcaoExcecao("Não foi possível localizar a ação");

        var investidores = acao.InvestidoresInteressados.Select(x => x.Investidor).ToList();

        foreach (var investidor in investidores)
        {
            var assunto = $"Alteracao Preco Ação {acao.Nome}";
            var mensagem = $"Preço da ação {acao.Nome} foi alterado para: {acao.ValorAtual} reais";
            
            emailServico.EnviarEmail(
                destinatario: investidor.Email,
                assunto: assunto,
                mensagem: mensagem);
        }
    }
}