using B3.Dominio.Contratos;
using B3.Dominio.Enumeradores;
using B3.Dominio.Excecoes;
using B3.Dominio.Interfaces;
using B3.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("B3.Testes")]
namespace B3.Dominio.Servicos.Servicos;

internal sealed class OrdemServico : IOrdemServico
{
    private readonly IRepositorioBase repositorio;
    private readonly IInvestidorServico investidorServico;

    public OrdemServico(IRepositorioBase repositorio, IInvestidorServico investidorServico)
    {
        this.repositorio = repositorio;
        this.investidorServico = investidorServico;
    }

    public async Task<RetornoRegistrarOrdemContrato> RegistrarOrdemAsync(RegistrarOrdemContrato contrato, CancellationToken cancellationToken = default)
    {
        if (contrato.Valor <= 0)
            throw new OrdemExcecao("O valor da ordem deve ser maior que zero.");

        var investidor = await repositorio.MontarConsultar<Investidor>()
            .Where(x => x.Id == contrato.InvestidorId)
            .FirstOrDefaultAsync(cancellationToken);

        if (investidor is null)
            throw new InvestidorExcecao("Investidor não encontrado.");

        var acao = await repositorio.MontarConsultar<Acao>()
            .Where(x => x.Id == contrato.AcaoId)
            .FirstOrDefaultAsync(cancellationToken);

        if (acao is null)
            throw new AcaoExcecao("Ação não encontrada.");

        var ordem = new Ordem(
            investidorId: contrato.InvestidorId,
            acaoId: contrato.AcaoId,
            tipo: contrato.Tipo,
            valor: contrato.Valor);

        await repositorio.AdicionarAsync(ordem, cancellationToken);

        var ordemContraria = await repositorio.MontarConsultar<Ordem>()
            .Where(x => x.AcaoId == contrato.AcaoId
                     && x.Id != ordem.Id
                     && x.Status == StatusOrdem.Pendente
                     && x.Tipo != contrato.Tipo
                     && x.Tipo != TipoOrdem.NaoIdentificado)
            .OrderBy(x => x.DataCriacao)
            .FirstOrDefaultAsync(cancellationToken);

        if (ordemContraria is not null)
        {
            ordem.Finalizar();
            ordemContraria.Finalizar();

            acao.ValorAtual = contrato.Valor;

            using var transacao = await repositorio.IniciarTransacaoAsync(cancellationToken);
            try
            {
                ordem.Finalizar();
                ordemContraria.Finalizar();
                acao.ValorAtual = ordem.Valor;

                await repositorio.AtualizarAsync(ordem, cancellationToken);
                await repositorio.AtualizarAsync(ordemContraria, cancellationToken);
                await repositorio.AtualizarAsync(acao, cancellationToken);

                await transacao.CommitAsync(cancellationToken);
            }
            catch
            {
                await transacao.RollbackAsync(cancellationToken);
                throw;
            }

            await investidorServico.NotificarInvestidoresInteressadosNaAcaoAsync(acao.Id, cancellationToken);

            return new RetornoRegistrarOrdemContrato(
                OrdemId: ordem.Id,
                AcaoId: acao.Id,
                ValorAcao: acao.ValorAtual,
                Status: ordem.Status,
                HouveMatch: true,
                OrdemContrariaId: ordemContraria.Id);
        }

        return new RetornoRegistrarOrdemContrato(
            OrdemId: ordem.Id,
            AcaoId: acao.Id,
            ValorAcao: acao.ValorAtual,
            Status: ordem.Status,
            HouveMatch: false,
            OrdemContrariaId: null);
    }
}