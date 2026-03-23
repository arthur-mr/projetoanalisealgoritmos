using B3.Dominio.Interfaces;
using B3.Dominio.Modelos.Base;
using B3.Infra.Contextos;
using Microsoft.EntityFrameworkCore.Storage;

namespace B3.Infra.Repositorio;

public sealed class RepositorioBase : IRepositorioBase
{
    private readonly Contexto contexto;

    public RepositorioBase(Contexto contexto)
    {
        this.contexto = contexto;
    }

    public async Task<IDbContextTransaction> IniciarTransacaoAsync(CancellationToken cancellationToken = default)
    {
        return await contexto.Database.BeginTransactionAsync(cancellationToken);
    }

    public IQueryable<T> MontarConsultar<T>() where T : ModeloBase
    {
        return contexto.Set<T>().AsQueryable();
    }

    public async Task AdicionarAsync<T>(T entidade, CancellationToken cancellationToken = default) where T : ModeloBase
    {
        await contexto.Set<T>().AddAsync(entidade, cancellationToken);
        await contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync<T>(T entidade, CancellationToken cancellationToken = default) where T : ModeloBase
    {
        contexto.Set<T>().Update(entidade);
        await contexto.SaveChangesAsync(cancellationToken);
    }
}