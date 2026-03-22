using Trabalho1.Enumeradores;
using Trabalho1.Interfaces;

namespace Trabalho1.Servicos.Calculadores;

internal sealed class CalculadorEntregaRetiradaLocal : ICalculadorEntregas
{
    public ModalidadeEntrega Modalidade => ModalidadeEntrega.RetiradaNoLocal;

    public decimal Calcular(decimal pesoTotalEmKg) => 0m;
}