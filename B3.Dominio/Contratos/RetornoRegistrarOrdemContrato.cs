using B3.Dominio.Enumeradores;

namespace B3.Dominio.Contratos;

public sealed record RetornoRegistrarOrdemContrato(
    Guid OrdemId,
    Guid AcaoId,
    decimal ValorAcao,
    StatusOrdem Status,
    bool HouveMatch,
    Guid? OrdemContrariaId);
