using B3.Dominio.Excecoes;
using B3.Dominio.Interfaces;
using B3.Dominio.Modelos;
using B3.Dominio.Modelos.Base;
using B3.Dominio.Servicos.Servicos;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace B3.Testes.Servicos;

public sealed class AcaoServicoTestes : IDisposable
{
    private readonly ContextoTeste contexto;
    private readonly Mock<IEmailServico> emailMock;
    private readonly IRepositorioBase repositorio;
    private readonly IAcaoServico servico;

    public AcaoServicoTestes()
    {
        var options = new DbContextOptionsBuilder<ContextoTeste>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        contexto = new ContextoTeste(options);
        emailMock = new Mock<IEmailServico>();

        repositorio = new RepositorioBaseFake(contexto, null);
        servico = new AcaoServico(emailMock.Object, repositorio);
    }

    public void Dispose()
    {
        contexto.Dispose();
    }

    [Fact]
    public async Task NotificarAlteracaoQuandoAcaoNaoExistirDeveLancarExcecao()
    {
        var id = Guid.NewGuid();

        var ex = await Assert.ThrowsAsync<AcaoExcecao>(() =>
            servico.NotificarAlteracaoParaInvestidoresAsync(id, CancellationToken.None));

        Assert.Equal("Não foi possível localizar a ação", ex.Message);

        emailMock.Verify(x => x.EnviarEmail(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task NotificarAlteracaoQuandoNaoHouverInvestidoresNaoDeveEnviarEmail()
    {
        var acao = CriarAcao("PETR4", 30m);

        contexto.Acoes.Add(acao);
        await contexto.SaveChangesAsync();

        await servico.NotificarAlteracaoParaInvestidoresAsync(acao.Id, CancellationToken.None);

        emailMock.Verify(x => x.EnviarEmail(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task NotificarAlteracaoQuandoHouverInvestidoresDeveEnviarEmailParaTodos()
    {
        var acao = CriarAcao("PETR4", 30m);

        var investidor1 = new Investidor("Arthur", "arthur@email.com");
        var investidor2 = new Investidor("Maria", "maria@email.com");

        contexto.Investidores.AddRange(investidor1, investidor2);
        contexto.Acoes.Add(acao);
        await contexto.SaveChangesAsync();

        var rel1 = new AcaoInvestidor(acao.Id, investidor1.Id)
        {
            Investidor = investidor1
        };

        var rel2 = new AcaoInvestidor(acao.Id, investidor2.Id)
        {
            Investidor = investidor2
        };

        acao.InvestidoresInteressados.Add(rel1);
        acao.InvestidoresInteressados.Add(rel2);

        await contexto.SaveChangesAsync();

        await servico.NotificarAlteracaoParaInvestidoresAsync(acao.Id, CancellationToken.None);

        emailMock.Verify(x => x.EnviarEmail(
            investidor1.Email,
            It.Is<string>(s => s.Contains("PETR4")),
            It.Is<string>(m => m.Contains("30"))),
            Times.Once);

        emailMock.Verify(x => x.EnviarEmail(
            investidor2.Email,
            It.Is<string>(s => s.Contains("PETR4")),
            It.Is<string>(m => m.Contains("30"))),
            Times.Once);

        emailMock.Verify(x => x.EnviarEmail(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()),
            Times.Exactly(2));
    }

    private static Acao CriarAcao(string nome, decimal valor)
    {
        var acao = (Acao)Activator.CreateInstance(typeof(Acao), true)!;
        acao.Nome = nome;
        acao.ValorAtual = valor;
        return acao;
    }
}