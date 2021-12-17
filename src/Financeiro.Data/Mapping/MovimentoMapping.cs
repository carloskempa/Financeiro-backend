using Financeiro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Data.Mapping
{
    public class MovimentoMapping : IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                   .IsRequired()
                   .HasColumnName("Descricao")
                   .HasColumnType("varchar(500)");

            builder.Property(c => c.ValorMovimento)
                   .IsRequired()
                   .HasColumnName("ValorMovimento")
                   .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Observacao)
                   .HasColumnName("Observacao")
                   .HasColumnType("varchar(1000)");

            builder.Property(c => c.IsPago)
                   .IsRequired()
                   .HasColumnName("IsPago")
                   .HasColumnType("bit");

            builder.Property(c => c.DataMovimento)
                   .IsRequired()
                   .HasColumnName("DataMovimento")
                   .HasColumnType("datetime");

            builder.Property(c => c.TipoMovimento)
                   .IsRequired()
                   .HasColumnName("TipoMovimento")
                   .HasColumnType("int");

            builder.Property(c => c.DataVencimento)
                   .HasColumnName("DataVencimento")
                   .HasColumnType("datetime");

            builder.HasOne(c => c.Conta)
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(c => c.ContaId);

            builder.HasOne(c => c.CentroCusto)
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(c => c.CentroCustoId);

            builder.HasOne(c => c.Pessoa)
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(c => c.PessoaId);

            builder.HasOne(c => c.PessoaPagador)
                   .WithMany(c => c.MovimentosPagador)
                   .HasForeignKey(c => c.PessoaPagadorId);

            builder.HasOne(c => c.Fornecedor)
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(c => c.FornecedorId);

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.DtAtualizacao)
                   .HasColumnName("DtAtualizacao")
                   .HasColumnType("datetime");

            builder.ToTable("Movimentos");
        }
    }
}
