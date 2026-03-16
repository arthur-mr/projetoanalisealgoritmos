using Trabalho1.Enumeradores;
using Trabalho1.Exceptions;
using Trabalho1.Interfaces;

namespace Trabalho1.Servicos.Calculadores;

internal sealed class CalculadorEntregaPac : ICalculadorEntregas
{
    private const decimal PesoMaximoKg = 2m;
    private const decimal ValorAte1Kg = 10m;
    private const decimal ValorAte2Kg = 15m;

    public ModalidadeEntrega Modalidade => ModalidadeEntrega.EncomendaPac;

    public decimal Calcular(decimal pesoTotalEmKg)
    {
        if (pesoTotalEmKg <= 0)
            throw new EntregaExcecao("O peso deve ser maior que zero.");

        if (pesoTotalEmKg > PesoMaximoKg)
            throw new EntregaExcecao("Peso acima do suportado para a modalidade PAC.");

        if (pesoTotalEmKg <= 1m)
            return ValorAte1Kg;

        return ValorAte2Kg;
    }
}
