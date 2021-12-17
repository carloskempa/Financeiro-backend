using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Financeiro.Data.Queries
{
    public class FornecedorQuery : IFornecedorQuery
    {
        private readonly FinanceiroContext _context;

        public FornecedorQuery(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<PaginatedRest<Fornecedor>> ListarTodos(Paginacao paginacao)
        {
            return await _context.Fornecedores.AsNoTracking().OrderByDescending(c => c.DtCadastro)
                                              .ToPaginatedRestAsync(paginacao.Page == 0 ? 1 : paginacao.Page,
                                                        paginacao.PageSize == 0 ? 10 : paginacao.PageSize);
        }
    }
}
