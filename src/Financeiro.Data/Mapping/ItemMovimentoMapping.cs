using Financeiro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Data.Mapping
{
    public class ItemMovimentoMapping : IEntityTypeConfiguration<ItemMovimento>
    {
        public void Configure(EntityTypeBuilder<ItemMovimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Historico)
                   .IsRequired()
                   .HasColumnName("Historico")
                   .HasColumnType($"varchar({ItemMovimento.HISTORICO_LENGHT})");

            builder.Property(c => c.ChaveLancamento)
                   .IsRequired()
                   .HasColumnName("ChaveLancamento")
                   .HasColumnType($"varchar({ItemMovimento.CHAVE_LANCAMENTO_LENGHT})");

            builder.Property(c => c.NumeroParcela)
                   .IsRequired()
                   .HasColumnName("NumeroParcela")
                   .HasColumnType("int");

            builder.Property(c => c.TotalParcela)
                   .IsRequired()
                   .HasColumnName("TotalParcela")
                   .HasColumnType("int");

            builder.Property(c => c.Valor)
                   .IsRequired()
                   .HasColumnName("Valor")
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(c => c.Movimento)
                   .WithMany(c => c.ItensMovimentos)
                   .HasForeignKey(c => c.MovimentoId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CentroCusto)
                   .WithMany(c => c.ItemMovimentos)
                   .HasForeignKey(c => c.CentroCustoId);

            builder.HasOne(c => c.Pessoa)
                   .WithMany(c => c.ItensMovimentos)
                   .HasForeignKey(c => c.PessoaId);

            builder.HasOne(c => c.PessoaPagador)
                   .WithMany(c => c.ItensMovimentosPagador)
                   .HasForeignKey(c => c.PessoaPagadorId);

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.DtAtualizacao)
                   .HasColumnName("DtAtualizacao")
                   .HasColumnType("datetime");

            builder.ToTable("ItemMovimento");
        }
    }
}
