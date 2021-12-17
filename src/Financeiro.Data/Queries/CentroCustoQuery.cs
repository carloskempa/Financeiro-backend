using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Financeiro.Data.Queries
{
    public class CentroCustoQuery : ICentroCustoQuery
    {
        private readonly FinanceiroContext _context;

        public CentroCustoQuery(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<PaginatedRest<CentroCusto>> ListarTodos(Paginacao paginacao)
        {
            return await _context.CentroCustos.AsNoTracking().OrderByDescending(c => c.DtCadastro)
                                              .ToPaginatedRestAsync(paginacao.Page == 0 ? 1 : paginacao.Page,
                                                                    paginacao.PageSize == 0 ? 10 : paginacao.PageSize);
        }
    }
}
