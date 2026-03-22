using Trabalho1.Enumeradores;
using Trabalho1.Exceptions;
using Trabalho1.Interfaces;

namespace Trabalho1.Servicos.Calculadores;

internal sealed class CalculadorEntregaSedex : ICalculadorEntregas
{
    private const decimal ValorAte500g = 12.5m;
    private const decimal ValorAte1Kg = 20m;
    private const decimal TaxaFixaAcima1Kg = 46.5m;
    private const decimal FaixaAdicionalKg = 0.1m;
    private const decimal ValorPorCada100gAdicional = 1.5m;

    public ModalidadeEntrega Modalidade => ModalidadeEntrega.Sedex;

    public decimal Calcular(decimal pesoTotalEmKg)
    {
        if (pesoTotalEmKg <= 0)
            throw new EntregaExcecao("O peso deve ser maior que zero.");

        if (pesoTotalEmKg <= 0.5m)
            return ValorAte500g;

        if (pesoTotalEmKg <= 1m)
            return ValorAte1Kg;

        var faixasAdicionais = (pesoTotalEmKg - 1m) / FaixaAdicionalKg;
        return TaxaFixaAcima1Kg + (faixasAdicionais * ValorPorCada100gAdicional);
    }
}
