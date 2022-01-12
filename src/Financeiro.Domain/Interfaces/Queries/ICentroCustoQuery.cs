using Canducci.Pagination;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Queries
{
    public interface ICentroCustoQuery
    {
        Task<PaginatedRest<CentroCusto>> ListarTodos(Paginacao paginacao);
        Task<IEnumerable<CentroCusto>> ListarTodos();

    }
}
