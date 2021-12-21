using Canducci.Pagination;
using Financeiro.App.Dtos;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IContaFinanceiraApp
    {
        Task<PaginatedRest<ContaFinanceira>> Listar(Paginacao paginacao);
        Task<RetornoPadrao<ContaFinanceiraDto>> ObterPorId(Guid id);
        Task<RetornoPadrao<ContaFinanceiraDto>> Cadastrar(ContaFinanceiraDto contaFinanceira);
        Task<RetornoPadrao<ContaFinanceiraDto>> Atualizar(ContaFinanceiraDto contaFinanceira);
        Task<RetornoPadrao<ContaFinanceiraDto>> Deletar(Guid id);
    }
}
