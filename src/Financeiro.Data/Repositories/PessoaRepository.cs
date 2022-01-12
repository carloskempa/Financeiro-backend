using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Financeiro.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly FinanceiroContext _context;

        public PessoaRepository(FinanceiroContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<Pessoa> ObterPorId(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null) 
                return null;

            await _context.Entry(pessoa)
                          .Collection(i => i.PessoaCentroCustos)
                          .LoadAsync();

            return pessoa;
        }

        public Task<Pessoa> ObterPorNome(string nome)
        {
            return _context.Pessoas.AsNoTracking().FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
        }


        public async Task<IEnumerable<PessoaCentroCusto>> Buscar(Expression<Func<PessoaCentroCusto, bool>> predicado)
        {
            return await _context.PessoaCentroCustos.AsNoTracking()
                                                    .Where(predicado)
                                                    .ToListAsync();
        }

        public void Cadastrar(Pessoa entity)
        {
            _context.Pessoas.Add(entity);
        }

        public void Atualizar(Pessoa entity)
        {
            _context.Pessoas.Update(entity);
        }

        public void Deletar(Pessoa entity)
        {
            _context.Pessoas.Remove(entity);
        }


        public void Cadastrar(PessoaCentroCusto entity)
        {
            _context.PessoaCentroCustos.Add(entity);
        }

        public void CadastrarRange(IEnumerable<PessoaCentroCusto> entities)
        {
            _context.PessoaCentroCustos.AddRange(entities);
        }

        public void Atualizar(PessoaCentroCusto entity)
        {
            _context.PessoaCentroCustos.Update(entity);
        }

        public void Deletar(PessoaCentroCusto entity)
        {
            _context.PessoaCentroCustos.Remove(entity);
        }

        public Task<PessoaCentroCusto> ObterPorIds(Guid pessoaId, Guid centroCustoId)
        {
            return _context.PessoaCentroCustos.AsNoTracking().FirstOrDefaultAsync(c => c.PessoaId == pessoaId && c.CentroCustoId == centroCustoId);
        }

        public void DeletarRangePessoaCentroCusto(IEnumerable<PessoaCentroCusto> entities)
        {
            _context.PessoaCentroCustos.RemoveRange(entities);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
