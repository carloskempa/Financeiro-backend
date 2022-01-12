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
    public class PessoaQuery : IPessoaQuery
    {
        private readonly FinanceiroContext _context;

        public PessoaQuery(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<PaginatedRest<Pessoa>> ListarTodos(Paginacao paginacao)
        {
            return await _context.Pessoas.AsNoTracking()
                                         .Include(c => c.PessoaCentroCustos)
                                         .ThenInclude(centro => centro.CentroCusto)
                                         .OrderByDescending(c => c.DtCadastro)
                                         .ToPaginatedRestAsync(paginacao.Page == 0 ? 1 : paginacao.Page, paginacao.PageSize == 0 ? 10 : paginacao.PageSize);
        }

        public async Task<IEnumerable<Pessoa>> ListarTodos()
        {
            return await _context.Pessoas.AsNoTracking().ToListAsync();
        }
    }
}
