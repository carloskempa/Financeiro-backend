using Financeiro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Data.Mapping
{
    public class PessoaCentroCustoMapping : IEntityTypeConfiguration<PessoaCentroCusto>
    {
        public void Configure(EntityTypeBuilder<PessoaCentroCusto> builder)
        {
            builder.HasKey(c => new { c.PessoaId, c.CentroCustoId });

            builder.HasOne(c => c.CentroCusto)
                   .WithMany(c => c.PessoaCentroCustos)
                   .HasForeignKey(c => c.CentroCustoId);

            builder.HasOne(c => c.Pessoa)
                   .WithMany(c => c.PessoaCentroCustos)
                   .HasForeignKey(c => c.PessoaId);

            builder.Property(c => c.ValorMensal)
                   .IsRequired()
                   .HasColumnName("ValorMensal")
                   .HasColumnType("decimal(18,2)");

            builder.ToTable("PessoaCentroCusto");
        }
    }
}
