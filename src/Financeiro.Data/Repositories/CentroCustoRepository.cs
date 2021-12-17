using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Financeiro.Data.Repositories
{
    public class CentroCustoRepository : ICentroCustoRepository
    {
        private readonly FinanceiroContext _context;

        public CentroCustoRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<CentroCusto> ObterPorId(Guid id)
        {
            return await _context.CentroCustos.FindAsync(id);
        }

        public void Cadastrar(CentroCusto entity)
        {
            _context.CentroCustos.Add(entity);
        }

        public void Atualizar(CentroCusto entity)
        {
            _context.CentroCustos.Update(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<CentroCusto> ObterPeloNome(string nome)
        {
            return await _context.CentroCustos.AsNoTracking().FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
        }
    }
}
