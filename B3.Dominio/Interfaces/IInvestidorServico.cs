using B3.Dominio.Contratos;

namespace B3.Dominio.Interfaces;

public interface IInvestidorServico
{
    Task InscreverInvestidorNaAcaoAsync(InscreverInvestidorEmAcaoContrato contrato, CancellationToken cancellationToken = default);
}