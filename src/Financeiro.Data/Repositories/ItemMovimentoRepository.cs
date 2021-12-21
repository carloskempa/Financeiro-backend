using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Data.Repositories
{
    public class ItemMovimentoRepository : IItemMovimentoRepository
    {
        private readonly FinanceiroContext _context;

        public ItemMovimentoRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<ItemMovimento>> ObterItensMovimento(Guid movimentoId)
        {
            return await _context.ItemMovimentos.AsNoTracking()
                                                .Where(c => c.MovimentoId == movimentoId)
                                                .Include(c=>c.Pessoa)
                                                .Include(c=>c.CentroCusto)
                                                .Include(c=>c.PessoaPagador).ToListAsync();
        }

        public async Task<IEnumerable<ItemMovimento>> Buscar(Expression<Func<ItemMovimento, bool>> predicado)
        {
            return await _context.ItemMovimentos.AsNoTracking()
                                                .Where(predicado)
                                                .ToListAsync();
        }

        public async Task<ItemMovimento> ObterPorId(Guid id)
        {
            return await _context.ItemMovimentos.FindAsync(id);
        }

        public void Atualizar(ItemMovimento entity)
        {
            _context.ItemMovimentos.Update(entity);
        }

        public void Cadastrar(ItemMovimento entity)
        {
            _context.ItemMovimentos.Add(entity);
        }

        public void Deletar(ItemMovimento entity)
        {
            _context.ItemMovimentos.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
