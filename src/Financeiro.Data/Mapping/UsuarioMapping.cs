using Financeiro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasColumnName("Nome")
                   .HasColumnType($"varchar({Usuario.NOME_LENGHT})");

            builder.Property(c => c.Login)
                   .IsRequired()
                   .HasColumnName("Login")
                   .HasColumnType($"varchar({Usuario.LOGIN_LENGHT})");

            builder.Property(c => c.Senha)
                   .IsRequired()
                   .HasColumnName("Senha")
                   .HasColumnType("varbinary(max)");

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasColumnName("Ativo")
                   .HasColumnType("bit");

            builder.Property(c => c.RefreshToken)
                   .HasColumnName("RefreshToken")
                   .HasColumnType("varchar(100)");

            builder.Property(c => c.DtCadastro)
                   .IsRequired()
                   .HasColumnName("DtCadastro")
                   .HasColumnType("datetime");

            builder.Property(c => c.DtAtualizacao)
                   .HasColumnName("DtAtualizacao")
                   .HasColumnType("datetime");

            builder.ToTable("Usuarios");
        }
    }
}
