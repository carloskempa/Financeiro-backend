using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Queries
{
    public interface IMovimentoQuery
    {
        Task<PaginatedRest<Movimento>> MovimentoFilter(MovimentoFilter filter, Paginacao paginacao);
    }
}
