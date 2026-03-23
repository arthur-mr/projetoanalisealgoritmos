using B3.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace B3.Testes.Servicos;

internal sealed class ContextoTeste : DbContext
{
    public ContextoTeste(DbContextOptions<ContextoTeste> options)
        : base(options)
    { }

    public DbSet<Acao> Acoes => Set<Acao>();
    public DbSet<Investidor> Investidores => Set<Investidor>();
    public DbSet<Ordem> Ordens => Set<Ordem>();
}