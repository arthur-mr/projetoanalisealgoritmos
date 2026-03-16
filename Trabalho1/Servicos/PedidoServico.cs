using Trabalho1.Contratos;
using Trabalho1.Interfaces;
using Trabalho1.Modelos;

namespace Trabalho1.Servicos;

internal sealed class PedidoServico : IPedidoServico
{
    private readonly ICalculadorEntregaServico calculadorEntregaServico;

    public PedidoServico(ICalculadorEntregaServico calculadorEntregaServico)
    {
        this.calculadorEntregaServico = calculadorEntregaServico;
    }

    public Pedido GerarPedido(SalvarPedidoContrato pedidoContrato)
    {
        var pedidoId = Guid.NewGuid();

        var livros = pedidoContrato.Livros?
            .Select(x => new Livro(pedidoId, x.Nome, x.Preco, x.Peso))
            .ToList();

        var pesoTotalEmKg = livros?.Sum(x => x.PesoEmKg) ?? 0;

        var valorEntrega = calculadorEntregaServico.CalcularValorEntrega(pedidoContrato.Modalidade, pesoTotalEmKg);
        
        var pedido = new Pedido(
            id: pedidoId,
            livros: livros,
            modalidadeEntrega: pedidoContrato.Modalidade,
            valorEntrega: valorEntrega);

        return pedido;
    }
}
