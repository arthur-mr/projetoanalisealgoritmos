using B3.Dominio.Contratos;
using B3.Dominio.Enumeradores;
using B3.Dominio.Excecoes;
using B3.Dominio.Interfaces;
using B3.Dominio.Mensagens;
using B3.Dominio.Modelos;
using B3.Dominio.Servicos.Servicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace B3.Testes.Servicos;

public sealed class OrdemServicoTestes : IDisposable
{
    private readonly ContextoTeste contexto;
    private readonly Mock<IDbContextTransaction> transacaoMock;
    private readonly Mock<IAcaoServico> acaoServicoMock;
    private readonly IRepositorioBase repositorio;
    private readonly IOrdemServico servico;

    public OrdemServicoTestes()
    {
        var options = new DbContextOptionsBuilder<ContextoTeste>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        contexto = new ContextoTeste(options);
        transacaoMock = new Mock<IDbContextTransaction>(MockBehavior.Loose);
        acaoServicoMock = new Mock<IAcaoServico>(MockBehavior.Loose);

        repositorio = new RepositorioBaseFake(contexto, transacaoMock.Object);
        servico = new OrdemServico(repositorio, acaoServicoMock.Object);
    }

    public void Dispose()
    {
        contexto.Dispose();
    }

    [Fact]
    public async Task RegistrarOrdemAsyncQuandoValorForInvalidoDeveLancarExcecao()
    {
        var contrato = new RegistrarOrdemContrato(
            InvestidorId: Guid.NewGuid(),
            AcaoId: Guid.NewGuid(),
            Tipo: TipoOrdem.Compra,
            Valor: 0m);

        var excecao = await Assert.ThrowsAsync<OrdemExcecao>(() =>
            servico.RegistrarOrdemAsync(contrato, CancellationToken.None));

        Assert.Equal("O valor da ordem deve ser maior que zero.", excecao.Message);
        acaoServicoMock.Verify(x => x.NotificarAlteracaoParaInvestidoresAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RegistrarOrdemAsyncQuandoInvestidorNaoExistirDeveLancarExcecao()
    {
        var acao = CriarAcao("BBAS3", 24m);
        contexto.Acoes.Add(acao);
        await contexto.SaveChangesAsync();

        var contrato = new RegistrarOrdemContrato(
            InvestidorId: Guid.NewGuid(),
            AcaoId: acao.Id,
            Tipo: TipoOrdem.Compra,
            Valor: 24m);

        var excecao = await Assert.ThrowsAsync<InvestidorExcecao>(() =>
            servico.RegistrarOrdemAsync(contrato, CancellationToken.None));

        Assert.Equal("Investidor não encontrado.", excecao.Message); 
        acaoServicoMock.Verify(x => x.NotificarAlteracaoParaInvestidoresAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RegistrarOrdemAsyncQuandoAcaoNaoExistirDeveLancarExcecao()
    {
        var investidor = new Investidor("Joaquim", "joaquim@email.com");
        contexto.Investidores.Add(investidor);
        await contexto.SaveChangesAsync();

        var contrato = new RegistrarOrdemContrato(
            InvestidorId: investidor.Id,
            AcaoId: Guid.NewGuid(),
            Tipo: TipoOrdem.Compra,
            Valor: 24m);

        var excecao = await Assert.ThrowsAsync<AcaoExcecao>(() =>
            servico.RegistrarOrdemAsync(contrato, CancellationToken.None));

        Assert.Equal("Ação não encontrada.", excecao.Message);
        acaoServicoMock.Verify(x => x.NotificarAlteracaoParaInvestidoresAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RegistrarOrdemAsyncQuandoNaoHouverMatchDeveRegistrarOrdemPendente()
    {
        var investidor = new Investidor("Joaquim", "joaquim@email.com");
        var acao = CriarAcao("BBAS3", 24m);

        contexto.Investidores.Add(investidor);
        contexto.Acoes.Add(acao);
        await contexto.SaveChangesAsync();

        var contrato = new RegistrarOrdemContrato(
            InvestidorId: investidor.Id,
            AcaoId: acao.Id,
            Tipo: TipoOrdem.Compra,
            Valor: 24m);

        var retorno = await servico.RegistrarOrdemAsync(contrato, CancellationToken.None);

        Assert.False(retorno.HouveMatch);
        Assert.Null(retorno.OrdemContrariaId);
        Assert.Equal(acao.Id, retorno.AcaoId);
        Assert.Equal(24m, retorno.ValorAcao);
        Assert.Equal(StatusOrdem.Pendente, retorno.Status);

        var ordemSalva = await contexto.Ordens.SingleAsync();
        Assert.Equal(StatusOrdem.Pendente, ordemSalva.Status);
        Assert.Equal(TipoOrdem.Compra, ordemSalva.Tipo);
        Assert.Equal(investidor.Id, ordemSalva.InvestidorId);
        Assert.Equal(acao.Id, ordemSalva.AcaoId);

        acaoServicoMock.Verify(x => x.NotificarAlteracaoParaInvestidoresAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        transacaoMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RegistrarOrdemAsyncQuandoHouverMatchDeveFinalizarOrdensAtualizarAcaoEPublicarMensagem()
    {
        var investidorCompra = new Investidor("Joaquim", "joaquim@email.com");
        var investidorVenda = new Investidor("Mariana", "mariana@email.com");
        var acao = CriarAcao("BBAS3", 23m);

        contexto.Investidores.AddRange(investidorCompra, investidorVenda);
        contexto.Acoes.Add(acao);

        var ordemVenda = new Ordem(
            investidorId: investidorVenda.Id,
            acaoId: acao.Id,
            tipo: TipoOrdem.Venda,
            valor: 24m);

        contexto.Ordens.Add(ordemVenda);
        await contexto.SaveChangesAsync();

        var contrato = new RegistrarOrdemContrato(
            InvestidorId: investidorCompra.Id,
            AcaoId: acao.Id,
            Tipo: TipoOrdem.Compra,
            Valor: 24m);

        var retorno = await servico.RegistrarOrdemAsync(contrato, CancellationToken.None);

        Assert.True(retorno.HouveMatch);
        Assert.Equal(ordemVenda.Id, retorno.OrdemContrariaId);
        Assert.Equal(acao.Id, retorno.AcaoId);
        Assert.Equal(24m, retorno.ValorAcao);
        Assert.Equal(StatusOrdem.Finalizada, retorno.Status);

        var ordens = await contexto.Ordens.OrderBy(x => x.DataCriacao).ToListAsync();
        Assert.Equal(2, ordens.Count);
        Assert.All(ordens, ordem =>
        {
            Assert.Equal(StatusOrdem.Finalizada, ordem.Status);
            Assert.NotNull(ordem.DataFinalizacao);
        });

        var acaoAtualizada = await contexto.Acoes.SingleAsync(x => x.Id == acao.Id);
        Assert.Equal(24m, acaoAtualizada.ValorAtual);

        acaoServicoMock.Verify(x => x.NotificarAlteracaoParaInvestidoresAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        transacaoMock.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        transacaoMock.Verify(x => x.RollbackAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    private static Acao CriarAcao(string nome, decimal valorAtual)
    {
        var acao = (Acao)Activator.CreateInstance(typeof(Acao), nonPublic: true)!;
        acao.Nome = nome;
        acao.ValorAtual = valorAtual;
        acao.InvestidoresInteressados ??= new List<AcaoInvestidor>();
        return acao;
    }
}