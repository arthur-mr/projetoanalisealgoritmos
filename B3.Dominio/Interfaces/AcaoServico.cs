namespace B3.Dominio.Interfaces;

public interface IAcaoServico
{
    Task NotificarAlteracaoParaInvestidoresAsync(Guid acaoId, CancellationToken cancellationToken);
}