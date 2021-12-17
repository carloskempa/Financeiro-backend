using Financeiro.Data.Mapping;
using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Financeiro.Data
{
    public class FinanceiroContext : DbContext, IUnitOfWork
    {
        public FinanceiroContext(DbContextOptions<FinanceiroContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ContaFinanceira> ContaFinanceiras { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<CentroCusto> CentroCustos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<PessoaCentroCusto> PessoaCentroCustos { get; set; }
        public DbSet<ItemMovimento> ItemMovimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new ContaFinanceiraMapping());
            modelBuilder.ApplyConfiguration(new FornecedorMapping());
            modelBuilder.ApplyConfiguration(new CentroCustoMapping());
            modelBuilder.ApplyConfiguration(new PessoaMapping());
            modelBuilder.ApplyConfiguration(new MovimentoMapping());
            modelBuilder.ApplyConfiguration(new PessoaCentroCustoMapping());
            modelBuilder.ApplyConfiguration(new ItemMovimentoMapping());
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DtCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DtCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DtCadastro").IsModified = false;
                    entry.Property("DtAtualizacao").CurrentValue = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
