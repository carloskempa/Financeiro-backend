using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Queries
{
    public interface IContaFinanceiraQuery
    {
        Task<PaginatedRest<ContaFinanceira>> ListarTodos(Paginacao paginacao);
    }
}
