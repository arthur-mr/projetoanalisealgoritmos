using Trabalho1.Enumeradores;

namespace Trabalho1.Interfaces;

public interface ICalculadorEntregas
{
    ModalidadeEntrega Modalidade { get; }
    decimal Calcular(decimal pesoTotalEmKg);
}