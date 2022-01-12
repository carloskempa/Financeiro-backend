using Canducci.Pagination;
using Financeiro.App.Dtos;
using Financeiro.App.Dtos.Pessoa;
using Financeiro.Domain.DataTransferObjects.Filtro;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financeiro.App.Interfaces
{
    public interface IPessoaApp
    {
        Task<PaginatedRest<Pessoa>> Listar(Paginacao paginacao);
        Task<IEnumerable<PessoaDto>> ListarTodos();
        Task<RetornoPadrao<PessoaDto>> ObterPorId(Guid id);
        Task<RetornoPadrao<PessoaCadastroDto>> Cadastrar(PessoaCadastroDto pessoa);
        Task<RetornoPadrao<PessoaDto>> Atualizar(PessoaDto pessoa);
        Task<RetornoPadrao<PessoaCadastroDto>> Deletar(Guid id);
    }
}
