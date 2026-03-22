using B3.Dominio.Contratos;
using B3.Dominio.Interfaces;
using B3.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace B3.Dominio.Servicos.Servicos;

internal sealed class InvestidorServico : IInvestidorServico
{
    private readonly IRepositorioBase repositorio;

    public InvestidorServico(IRepositorioBase repositorio)
    {
        this.repositorio = repositorio;
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
}
