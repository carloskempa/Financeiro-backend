using Canducci.Pagination;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.CentroCusto;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface ICentroCustoApp
    {
        Task<RetornoPadrao<CentroCustoDto>> ObterPorId(Guid id);
        Task<RetornoPadrao<CentroCustoDto>> Cadastrar(CentroCustoDto centroCusto);
        Task<RetornoPadrao<CentroCustoDto>> Atualizar(CentroCustoDto centroCusto);
        Task<PaginatedRest<CentroCusto>> Listar(Paginacao paginacao);
        Task<IEnumerable<CentroCustoDto>> ListarTodos();
        Task<RetornoPadrao<CentroCustoDto>> Deletar(Guid id);
    }
}
