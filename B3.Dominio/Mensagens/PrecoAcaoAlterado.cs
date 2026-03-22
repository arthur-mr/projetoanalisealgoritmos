using B3.Dominio.Utils;

namespace B3.Dominio.Mensagens;

[Fila("TarefaCriada")]
public sealed record PrecoAcaoAlterado(Guid AcaoId);