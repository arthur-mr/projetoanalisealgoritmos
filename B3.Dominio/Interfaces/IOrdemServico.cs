using B3.Dominio.Contratos;

namespace B3.Dominio.Interfaces;

public interface IOrdemServico
{
    Task<RetornoRegistrarOrdemContrato> RegistrarOrdemAsync(RegistrarOrdemContrato contrato, CancellationToken cancellationToken = default);
}