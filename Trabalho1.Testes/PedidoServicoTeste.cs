using Microsoft.Extensions.DependencyInjection;
using Trabalho1.Contratos;
using Trabalho1.Enumeradores;
using Trabalho1.Exceptions;
using Trabalho1.Interfaces;

namespace Trabalho1.Testes;

public class PedidoServicoTeste
{
    private readonly IPedidoServico pedidoServico;
    private readonly IServiceCollection services = new ServiceCollection();

    public PedidoServicoTeste()
    {
        services.AdicionarServicos();

        var provider = services.BuildServiceProvider();
        pedidoServico = provider.GetRequiredService<IPedidoServico>();
    }

    [Fact]
    public void Deve_Gerar_Pedido_Com_Entrega_Pac_Quando_Peso_Total_For_Ate_1Kg()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.EncomendaPac,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 50m, 0.4m),
                new("Livro B", 40m, 0.6m)
            }
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(10m, pedido.ValorEntrega);
    }

    [Fact]
    public void Deve_Gerar_Pedido_Com_Entrega_Pac_Quando_Peso_Total_For_Maior_Que_1Kg_E_Ate_2Kg()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.EncomendaPac,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 50m, 1.2m),
                new("Livro B", 40m, 0.3m)
            }
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(15m, pedido.ValorEntrega);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Pac_For_Acima_De_2Kg()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.EncomendaPac,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 50m, 1.5m),
                new("Livro B", 40m, 0.8m)
            }
        );

        var excecao = Assert.Throws<EntregaExcecao>(() => pedidoServico.GerarPedido(contrato));

        Assert.Equal("Peso acima do suportado para a modalidade PAC.", excecao.Message);
    }

    [Fact]
    public void Deve_Gerar_Pedido_Com_Entrega_Sedex_Quando_Peso_Total_For_Ate_500g()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.Sedex,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 30m, 0.2m),
                new("Livro B", 20m, 0.3m)
            }
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(12.5m, pedido.ValorEntrega);
    }

    [Fact]
    public void Deve_Gerar_Pedido_Com_Entrega_Sedex_Quando_Peso_Total_For_Maior_Que_500g_E_Ate_1Kg()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.Sedex,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 30m, 0.7m),
                new("Livro B", 20m, 0.2m)
            }
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(20m, pedido.ValorEntrega);
    }

    [Fact]
    public void Deve_Gerar_Pedido_Com_Entrega_Sedex_Quando_Peso_Total_For_Acima_De_1Kg()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.Sedex,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 30m, 1.2m)
            }
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(49.5m, pedido.ValorEntrega);
    }

    [Fact]
    public void Deve_Gerar_Pedido_Sem_Custo_Quando_For_Retirada_No_Local()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.RetiradaNoLocal,
            new List<SalvarLivroContrato>
            {
                new("Livro A", 100m, 3m),
                new("Livro B", 50m, 2m)
            }
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(0m, pedido.ValorEntrega);
    }

    [Fact]
    public void Deve_Gerar_Pedido_Sem_Custo_Quando_Retirada_No_Local_E_Sem_Livros()
    {
        var contrato = new SalvarPedidoContrato(
            ModalidadeEntrega.RetiradaNoLocal,
            new List<SalvarLivroContrato>()
        );

        var pedido = pedidoServico.GerarPedido(contrato);

        Assert.Equal(0m, pedido.ValorEntrega);
    }
}