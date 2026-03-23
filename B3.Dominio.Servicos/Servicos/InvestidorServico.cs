using B3.Dominio.Contratos;
using B3.Dominio.Excecoes;
using B3.Dominio.Interfaces;
using B3.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace B3.Dominio.Servicos.Servicos;

internal sealed class InvestidorServico : IInvestidorServico
{
    private readonly IRepositorioBase repositorio;
    private readonly IEmailServico emailServico;

    public InvestidorServico(IRepositorioBase repositorio, IEmailServico emailServico)
    {
        this.repositorio = repositorio;
        this.emailServico = emailServico;
    }

    public async Task InscreverInvestidorNaAcaoAsync(InscreverInvestidorEmAcaoContrato contrato, CancellationToken cancellationToken = default)
    {
        var investidor = await repositorio.MontarConsultar<Investidor>()
            .Include(x => x.AcoesInscritas)
            .Where(x => x.Id == contrato.InvestidorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (investidor is null)
            throw new InvalidOperationException("Investidor não encontrado.");

        var acao = await repositorio.MontarConsultar<Acao>()
            .Where(x => x.Id == contrato.AcaoId)
            .FirstOrDefaultAsync(cancellationToken);

        if (acao is null)
            throw new InvalidOperationException("Ação não encontrada.");

        if (!investidor.AcoesInscritas.Any(a => a.Id == acao.Id))
        {
            var acaoInvestidor = new AcaoInvestidor(acaoId: acao.Id, investidorId: investidor.Id);
            await repositorio.AdicionarAsync(acaoInvestidor, cancellationToken);
        }
    }

    public async Task NotificarInvestidoresInteressadosNaAcaoAsync(Guid acaoId, CancellationToken cancellationToken)
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
