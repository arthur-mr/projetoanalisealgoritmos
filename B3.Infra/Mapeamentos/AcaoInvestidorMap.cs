using B3.Dominio.Modelos;
using B3.Dominio.Modelos.Base;
using B3.Infra.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B3.Infra.Mapeamentos;

internal sealed class AcaoInvestidorMap : IEntityTypeConfiguration<AcaoInvestidor>
{
    private const string NOME_TABELA = "ACAO_INVESTIDOR";

    public void Configure(EntityTypeBuilder<AcaoInvestidor> builder)
    {
        MapeamentoComum.MapearComum(builder);
        builder.ToTable(NOME_TABELA);
        builder.Property(x => x.AcaoId).HasColumnName("ACAO_FK").IsRequired();
        builder.Property(x => x.InvestidorId).HasColumnName("INVESTIDOR_FK").IsRequired();

        builder.HasOne(x => x.Investidor)
            .WithMany()
            .HasForeignKey(y => y.InvestidorId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}

internal sealed class AcaoMap : IEntityTypeConfiguration<Acao>
{
    private const string NOME_TABELA = "ACAO";

    public void Configure(EntityTypeBuilder<Acao> builder)
    {
        MapeamentoComum.MapearComum(builder);
        builder.ToTable(NOME_TABELA);
        builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
        builder.Property(x => x.ValorAtual).HasColumnName("VALOR_ATUAL").IsRequired();

        builder.HasMany(x => x.InvestidoresInteressados)
            .WithOne()
            .HasForeignKey(y => y.AcaoId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}