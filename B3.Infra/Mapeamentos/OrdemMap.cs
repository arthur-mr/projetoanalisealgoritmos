using B3.Dominio.Modelos;
using B3.Infra.Mapeamentos.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace B3.Infra.Mapeamentos;

internal sealed class OrdemMap : IEntityTypeConfiguration<Ordem>
{
    private const string NOME_TABELA = "ORDEM";

    public void Configure(EntityTypeBuilder<Ordem> builder)
    {
        MapeamentoComum.MapearComum(builder);
        builder.ToTable(NOME_TABELA);
        builder.Property(x => x.InvestidorId).HasColumnName("INVESTIDOR_FK").IsRequired();
        builder.Property(x => x.AcaoId).HasColumnName("ACAO_FK").IsRequired();
        builder.Property(x => x.Tipo).HasColumnName("TIPO").IsRequired();
        builder.Property(x => x.Status).HasColumnName("STATUS").IsRequired();
        builder.Property(x => x.DataCriacao).HasColumnName("DATA_CRIACAO").IsRequired();
        builder.Property(x => x.DataFinalizacao).HasColumnName("DATA_FINALIZACAO").IsRequired(false);
        builder.Property(x => x.Valor).HasColumnName("VALOR").IsRequired(false);

        builder.HasOne(x => x.Investidor)
            .WithMany(x => x.Ordens)
            .HasForeignKey(x => x.InvestidorId)
            .IsRequired(false);
    }
}
