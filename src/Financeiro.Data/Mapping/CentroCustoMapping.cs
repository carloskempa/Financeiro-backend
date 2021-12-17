using Financeiro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Data.Mapping
{
    public class CentroCustoMapping : IEntityTypeConfiguration<CentroCusto>
    {
        public void Configure(EntityTypeBuilder<CentroCusto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType($"varchar({CentroCusto.NOME_LENGHT})");

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.DtAtualizacao)
                   .HasColumnName("DtAtualizacao")
                   .HasColumnType("datetime");

            builder.ToTable("CentroCusto");
        }
    }
}
