using B3.Dominio.Modelos.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace B3.Dominio.Interfaces;

public interface IRepositorioBase
{
    Task<IDbContextTransaction> IniciarTransacaoAsync(CancellationToken cancellationToken = default);

    IQueryable<T> MontarConsultar<T>() where T : ModeloBase;

    Task AdicionarAsync<T>(T entidade, CancellationToken cancellationToken = default) where T : ModeloBase;

    Task AtualizarAsync<T>(T entidade, CancellationToken cancellationToken = default) where T : ModeloBase;
}