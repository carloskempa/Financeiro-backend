using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Financeiro.Data.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly FinanceiroContext _context;

        public FornecedorRepository(FinanceiroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        
        public async Task<Fornecedor> ObterPorId(Guid id)
        {
            return await _context.Fornecedores.FindAsync(id);
        }

        public async Task<Fornecedor> ObterPorNome(string nome)
        {
            return await _context.Fornecedores.AsNoTracking().FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
        }

        public void Cadastrar(Fornecedor entity)
        {
            _context.Fornecedores.Add(entity);
        }

        public void Atualizar(Fornecedor entity)
        {
            _context.Fornecedores.Update(entity);
        }

        public void Deletar(Fornecedor entity)
        {
            _context.Fornecedores.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
