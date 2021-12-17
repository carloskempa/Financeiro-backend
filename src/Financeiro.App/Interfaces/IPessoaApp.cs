using Canducci.Pagination;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.Pessoa;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IPessoaApp
    {
        Task<PaginatedRest<Pessoa>> Listar(Paginacao paginacao);
        Task<RetornoPadrao<PessoaDto>> ObterPorId(Guid id);
        Task<RetornoPadrao<PessoaCadastroDto>> Cadastrar(PessoaCadastroDto pessoa);
        Task<RetornoPadrao<PessoaCadastroDto>> Atualizar(PessoaCadastroDto pessoa);
    }
}
