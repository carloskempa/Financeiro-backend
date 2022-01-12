using Financeiro.Domain.Core.Data;
using Financeiro.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Financeiro.Domain.Interfaces.Respositories
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
        Task<Pessoa> ObterPorNome(string Nome);
        void Cadastrar(Pessoa entity);
        void Atualizar(Pessoa entity);
        void Deletar(Pessoa entity);

        Task<PessoaCentroCusto> ObterPorIds(Guid pessoaId, Guid centroCustoId);
        Task<IEnumerable<PessoaCentroCusto>> Buscar(Expression<Func<PessoaCentroCusto, bool>> predicado);
        void Cadastrar(PessoaCentroCusto entity);
        void CadastrarRange(IEnumerable<PessoaCentroCusto> entities);
        void Atualizar(PessoaCentroCusto entity);
        void Deletar(PessoaCentroCusto entity);
        void DeletarRangePessoaCentroCusto(IEnumerable<PessoaCentroCusto> entities);
    }
}
