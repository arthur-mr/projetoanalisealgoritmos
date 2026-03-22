using B3.Dominio.Enumeradores;

namespace B3.Dominio.Contratos;

public sealed record RegistrarOrdemContrato(
    Guid InvestidorId,
    Guid AcaoId,
    TipoOrdem Tipo,
    decimal Valor);