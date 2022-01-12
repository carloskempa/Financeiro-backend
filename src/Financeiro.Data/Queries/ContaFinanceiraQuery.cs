using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financeiro.Data.Queries
{
    public class ContaFinanceiraQuery : IContaFinanceiraQuery
    {
        private readonly FinanceiroContext _context;

        public ContaFinanceiraQuery(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<PaginatedRest<ContaFinanceira>> ListarTodos(Paginacao paginacao)
        {
            return await _context.ContaFinanceiras.AsNoTracking()
                             .OrderByDescending(c => c.DtCadastro)
                             .ToPaginatedRestAsync(paginacao.Page == 0 ? 1 : paginacao.Page, paginacao.PageSize == 0 ? 10 : paginacao.PageSize);
        }

        public async Task<IEnumerable<ContaFinanceira>> ListarTodos()
        {
            return await _context.ContaFinanceiras.AsNoTracking().ToListAsync();
        }
    }
}
