using Canducci.Pagination;
using Financeiro.App.Dtos;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IItemMovimentoApp
    {
        Task<ItemMovimentoDto> Cadastrar(ItemMovimentoDto itemMovimentoDto);
        Task<PaginatedRest<ItemMovimento>> Listar(Guid MovimentoId);
    }
}
