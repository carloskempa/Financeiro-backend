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
    public interface IFornecedorQuery
    {
        Task<PaginatedRest<Fornecedor>> ListarTodos(Paginacao paginacao);
        Task<IEnumerable<Fornecedor>> ListarTodos();
    }
}
