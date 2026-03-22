using B3.Dominio.Modelos;
using B3.Infra.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B3.Infra.Mapeamentos;

internal sealed class InvestidorMap : IEntityTypeConfiguration<Investidor>
{
    private const string NOME_TABELA = "INVESTIDOR";

    public void Configure(EntityTypeBuilder<Investidor> builder)
    {
        MapeamentoComum.MapearComum(builder);
        builder.ToTable(NOME_TABELA);
        builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
        builder.Property(x => x.Email).HasColumnName("EMAIL").IsRequired();

        builder.HasMany(x => x.Ordens)
            .WithOne(x => x.Investidor)
            .HasForeignKey(y => y.InvestidorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        builder.HasMany(x => x.AcoesInscritas)
            .WithOne(x => x.Investidor)
            .HasForeignKey(y => y.InvestidorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}
