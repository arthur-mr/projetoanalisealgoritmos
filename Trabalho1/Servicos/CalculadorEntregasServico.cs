using Trabalho1.Enumeradores;
using Trabalho1.Exceptions;
using Trabalho1.Interfaces;
using Trabalho1.Servicos.Calculadores;

namespace Trabalho1.Servicos;

internal sealed class CalculadorEntregasServico : ICalculadorEntregaServico
{
    private readonly IReadOnlyDictionary<ModalidadeEntrega, ICalculadorEntregas> Calculadores;

    public CalculadorEntregasServico()
    {
        var estrategias = new ICalculadorEntregas[]
        {
            new CalculadorEntregaPac(),
            new CalculadorEntregaSedex(),
            new CalculadorEntregaRetiradaLocal()
        };

        Calculadores = estrategias.ToDictionary(x => x.Modalidade);
    }

    public decimal CalcularValorEntrega(ModalidadeEntrega modalidade, decimal pesoTotalEmKg)
    {
        if (!Calculadores.TryGetValue(modalidade, out var calculador))
            throw new EntregaExcecao($"Modalidade de entrega {modalidade} não mapeada.");

        return calculador.Calcular(pesoTotalEmKg);
    }
}