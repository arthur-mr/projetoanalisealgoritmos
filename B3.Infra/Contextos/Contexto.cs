using Microsoft.EntityFrameworkCore;

namespace B3.Infra.Contextos;

public sealed class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options)
    : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);
    }
}
