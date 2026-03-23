using B3.Dominio.Interfaces;
using B3.Dominio.Modelos.Base;
using Microsoft.EntityFrameworkCore.Storage;

namespace B3.Testes.Servicos;

internal sealed class RepositorioBaseFake : IRepositorioBase
{
    private readonly ContextoTeste contexto;
    private readonly IDbContextTransaction transacao;

    public RepositorioBaseFake(ContextoTeste contexto, IDbContextTransaction transacao)
    {
        this.contexto = contexto;
        this.transacao = transacao;
    }

    public IQueryable<T> MontarConsultar<T>() where T : ModeloBase
        => contexto.Set<T>().AsQueryable();

    public async Task AdicionarAsync<T>(T modelo, CancellationToken cancellationToken = default)
        where T : ModeloBase
    {
        await contexto.Set<T>().AddAsync(modelo, cancellationToken);
        await contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync<T>(T modelo, CancellationToken cancellationToken = default)
        where T : ModeloBase
    {
        contexto.Set<T>().Update(modelo);
        await contexto.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> IniciarTransacaoAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(transacao);
}