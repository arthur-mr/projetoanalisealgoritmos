using Trabalho1.Enumeradores;

namespace Trabalho1.Interfaces;

internal interface ICalculadorEntregaServico
{
    decimal CalcularValorEntrega(ModalidadeEntrega modalidade, decimal pesoTotalEmKg);
}