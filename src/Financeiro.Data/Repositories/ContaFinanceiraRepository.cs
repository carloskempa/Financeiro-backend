using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financeiro.Data.Repositories
{
    public class ContaFinanceiraRepository : IContaFinanceiraRepository
    {
        private readonly FinanceiroContext _context;

        public ContaFinanceiraRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ContaFinanceira> ObterPorId(Guid id)
        {
            return await _context.ContaFinanceiras.FindAsync(id);
        }

        public void Cadastrar(ContaFinanceira entity)
        {
            _context.ContaFinanceiras.Add(entity);
        }

        public void Atualizar(ContaFinanceira entity)
        {
            _context.ContaFinanceiras.Update(entity);
        }

        public void Deletar(ContaFinanceira entity)
        {
            _context.ContaFinanceiras.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
